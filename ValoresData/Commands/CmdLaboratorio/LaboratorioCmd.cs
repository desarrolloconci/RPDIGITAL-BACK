using System.Collections.Concurrent;
using System.Data;
using Dapper;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos.LaboratorioDto;

namespace ValoresData.Commands.CmdLaboratorio
{
    // Epic 2: reemplaza el loop secuencial de 63 llamadas (90% del tiempo de servidor en la
    // traza original) por 1 llamada + N llamadas en paralelo con concurrencia limitada.
    // No se crea ni modifica ningun SP - pa_HistoriaPrestacional_LaboratorioResultado y
    // pa_HistoriaPrestacional_LaboratorioResultadoValorxDet se invocan 1:1, solo cambia
    // como se orquestan (ver auditoria de Sprint 0: ambos son de solo lectura, con tablas
    // temporales locales - seguros para correr en paralelo).
    public class LaboratorioCmd : ILaboratorioCmd
    {
        private readonly IGeclisaConnectionFactory _connectionFactory;

        public LaboratorioCmd(IGeclisaConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<LaboratorioCompletoDto> GetLaboratorioCompletoAsync(
            int fichaId, DateTime fechaDesde, DateTime fechaHasta, string meArea,
            bool soloValidado, int? asId, int maxConcurrencia)
        {
            var lista = (await GetLaboratorioResultadoAsync(fichaId, fechaDesde, fechaHasta, meArea, soloValidado, asId)).ToList();

            var determinaciones = new ConcurrentBag<LaboratorioDeterminacionDto>();
            var errores = new ConcurrentBag<int>();

            using var throttle = new SemaphoreSlim(maxConcurrencia);

            var tareas = lista.Select(async det =>
            {
                await throttle.WaitAsync();
                try
                {
                    var valores = await GetLaboratorioValorxDetAsync(fechaDesde, fechaHasta, fichaId, det.Id, meArea, soloValidado);
                    determinaciones.Add(new LaboratorioDeterminacionDto { Determinacion = det, Valores = valores.ToList() });
                }
                catch (Exception)
                {
                    // Fail-soft: un det_id que falla no tumba el resto de la respuesta.
                    errores.Add(det.Id);
                }
                finally
                {
                    throttle.Release();
                }
            });

            await Task.WhenAll(tareas);

            return new LaboratorioCompletoDto
            {
                Determinaciones = determinaciones
                    .OrderBy(d => d.Determinacion.TipoNom)
                    .ThenBy(d => d.Determinacion.Código)
                    .ThenBy(d => d.Determinacion.Orden)
                    .ToList(),
                ErroresParciales = errores.OrderBy(id => id).ToList()
            };
        }

        // pa_HistoriaPrestacional_LaboratorioResultado - sin cambios en la llamada (1 vez)
        private async Task<IEnumerable<LaboratorioResultadoDto>> GetLaboratorioResultadoAsync(
            int fichaId, DateTime fechaDesde, DateTime fechaHasta, string meArea, bool soloValidado, int? asId)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<LaboratorioResultadoDto>(
                "pa_HistoriaPrestacional_LaboratorioResultado",
                new
                {
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaHasta,
                    ficha_id = fichaId,
                    Area = meArea,
                    SoloValidado = soloValidado,
                    As_id = asId ?? 0
                },
                commandType: CommandType.StoredProcedure);
        }

        // pa_HistoriaPrestacional_LaboratorioResultadoValorxDet - sin cambios en la llamada
        // (esta es la que se dispara N veces en paralelo, una por det_id)
        private async Task<IEnumerable<LaboratorioValorDto>> GetLaboratorioValorxDetAsync(
            DateTime fechaDesde, DateTime fechaHasta, int fichaId, int detId, string area, bool soloValidado)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<LaboratorioValorDto>(
                "pa_HistoriaPrestacional_LaboratorioResultadoValorxDet",
                new
                {
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaHasta,
                    ficha_id = fichaId,
                    det_id = detId,
                    Area = area,
                    SoloValidado = soloValidado
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
