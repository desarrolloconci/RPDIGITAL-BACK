using System.Data;
using Dapper;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos.HistoriasClinicasDto;

namespace ValoresData.Commands.CmdHistoriasClinicas
{
    // Invoca 1:1 los SPs existentes de GECLISA para Historia Clinica (Epic 1: header/listado).
    // No se crea, modifica ni reemplaza ningun SP - solo se paraleliza la orquestacion de llamadas
    // que hoy el cliente dispara por separado.
    public class HistoriasClinicasCmd : IHistoriasClinicasCmd
    {
        private readonly IGeclisaConnectionFactory _connectionFactory;

        public HistoriasClinicasCmd(IGeclisaConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // Fase 1 de la traza: 3 variantes de "listar HC por ficha", disparadas en paralelo
        // (en la traza original se pedian secuencialmente y una de ellas, SoloPantallas,
        // se pedia duplicada - eso no se replica).
        public async Task<CargaInicialHistoriaClinicaDto> GetCargaInicialAsync(
            int fichaId, DateTime? fechaInicio, DateTime? fechaFin, string? tipoHc,
            string? tevCod, int? preId, int? panId, int? usuarioIdQueConsulta)
        {
            var tevCodTask = GetListadoTevCodAsync(fichaId, fechaInicio, fechaFin, preId, tevCod, tipoHc);
            var soloPantallasTask = GetListadoSoloPantallasAsync(fichaId, fechaInicio, fechaFin, preId, panId, tipoHc, tevCod);
            var listadoTask = GetListadoAsync(fichaId, fechaInicio, fechaFin, preId, panId, tipoHc, tevCod, usuarioIdQueConsulta);

            await Task.WhenAll(tevCodTask, soloPantallasTask, listadoTask);

            return new CargaInicialHistoriaClinicaDto
            {
                ListadoTevCod = (await tevCodTask).ToList(),
                ListadoSoloPantallas = (await soloPantallasTask).ToList(),
                Listado = (await listadoTask).ToList()
            };
        }

        // panet_HistoriasClinicas_BuscarPorFechaFichaPreIdTevCod - sin cambios en la llamada
        private async Task<IEnumerable<HistoriaClinicaListadoTevCodDto>> GetListadoTevCodAsync(
            int fichaId, DateTime? fechaInicio, DateTime? fechaFin, int? preId, string? tevCod, string? tipoHc)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<HistoriaClinicaListadoTevCodDto>(
                "panet_HistoriasClinicas_BuscarPorFechaFichaPreIdTevCod",
                new { fichaId, fechaInicio, fechaFin, preId, TevCod = tevCod, tipoHc },
                commandType: CommandType.StoredProcedure);
        }

        // panet_HistoriasClinicas_BuscarPorFechaFichaPreIdSoloPantallas - sin cambios en la llamada
        private async Task<IEnumerable<HistoriaClinicaListadoSoloPantallasDto>> GetListadoSoloPantallasAsync(
            int fichaId, DateTime? fechaInicio, DateTime? fechaFin, int? preId, int? panId, string? tipoHc, string? tevCod)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<HistoriaClinicaListadoSoloPantallasDto>(
                "panet_HistoriasClinicas_BuscarPorFechaFichaPreIdSoloPantallas",
                new { fichaId, fechaInicio, fechaFin, preId, panId, tipoHc, TevCod = tevCod },
                commandType: CommandType.StoredProcedure);
        }

        // panet_HistoriasClinicas_BuscarPorFechaFichaPreId - sin cambios en la llamada
        private async Task<IEnumerable<HistoriaClinicaListadoDto>> GetListadoAsync(
            int fichaId, DateTime? fechaInicio, DateTime? fechaFin, int? preId, int? panId, string? tipoHc, string? tevCod, int? usuarioIdQueConsulta)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<HistoriaClinicaListadoDto>(
                "panet_HistoriasClinicas_BuscarPorFechaFichaPreId",
                new { fichaId, fechaInicio, fechaFin, preId, panId, tipoHc, TevCod = tevCod, UsuarioIdQueConsulta = usuarioIdQueConsulta },
                commandType: CommandType.StoredProcedure);
        }

        // Detalle de "la nota seleccionada" (Fases 1,3,6,7,11 de la traza): BuscarPorID primero
        // (aporta Fec_Alta/Fec_Modi), despues FirmaDigital + BuscarPanId en paralelo.
        public async Task<NotaSeleccionadaDetalleDto> GetDetalleNotaAsync(int hcId)
        {
            var detalle = await GetDetallePorIdAsync(hcId);
            if (detalle == null)
            {
                return new NotaSeleccionadaDetalleDto();
            }

            var firmaTask = GetFirmaDigitalAsync("HC", hcId, detalle.Fec_Alta, detalle.Fec_Modi);
            var panIdTask = GetPanIdAsync(hcId);

            await Task.WhenAll(firmaTask, panIdTask);

            return new NotaSeleccionadaDetalleDto
            {
                Detalle = detalle,
                Firma = await firmaTask,
                PanId = await panIdTask
            };
        }

        // panet_HistoriasClinicas_BuscarPorID - sin cambios en la llamada
        private async Task<HistoriaClinicaDetalleDto?> GetDetallePorIdAsync(int hcId)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<HistoriaClinicaDetalleDto>(
                "panet_HistoriasClinicas_BuscarPorID",
                new { Hc_id = hcId },
                commandType: CommandType.StoredProcedure);
        }

        // panet_FirmaDigital - sin cambios en la llamada
        private async Task<FirmaDigitalDto?> GetFirmaDigitalAsync(string tevCod, int evId, DateTime fecAlta, DateTime? fecModi)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<FirmaDigitalDto>(
                "panet_FirmaDigital",
                new { tev_cod = tevCod, ev_id = evId, fecAlta, fecModi },
                commandType: CommandType.StoredProcedure);
        }

        // panet_HistoriasClinicas_BuscarPanId - sin cambios en la llamada
        private async Task<int?> GetPanIdAsync(int hcId)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<int?>(
                "panet_HistoriasClinicas_BuscarPanId",
                new { Hc_id = hcId },
                commandType: CommandType.StoredProcedure);
        }

        // panet_Usuarios_BuscarPorID - info del usuario logueado (GECLISA), no de la nota.
        // No depende de hcId: el frontend deberia cachearla por usuarioId (React Query),
        // no volver a pedirla en cada click como hacia el cliente actual.
        public async Task<UsuarioGeclisaDto?> GetUsuarioLogueadoAsync(int usuarioId)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<UsuarioGeclisaDto>(
                "panet_Usuarios_BuscarPorID",
                new { Usuario_id = usuarioId },
                commandType: CommandType.StoredProcedure);
        }

        // Epic 4: alta de nueva evolucion clinica (Fase 5 de la traza). Flujo secuencial
        // intencional (no paralelizar, el orden importa): verificar internacion -> insertar
        // HC -> loguear. Es la nota "version 1", por eso los campos "*Anterior" del log van
        // en null (no habia nada antes) y Usu_Modi/Fec_Modi de la HC tambien van en null
        // (todavia no fue editada).
        public async Task<AltaEvolucionResponseDto> InsertarEvolucionAsync(AltaEvolucionRequestDto request)
        {
            var fecha = request.Fecha ?? DateTime.Today;
            var hora = request.Hora ?? (DateTime.Now.Hour * 100 + DateTime.Now.Minute);

            var meIdInternacion = await VerificarSiEstaInternadoAsync(request.FichaId, fecha, hora);

            var hcId = await InsertarHistoriaClinicaAsync(request, fecha, hora);

            var logHcId = await InsertarLogHcAsync(request, hcId, fecha, hora);

            return new AltaEvolucionResponseDto
            {
                HcId = hcId,
                LogHcId = logHcId,
                YaEstabaInternado = meIdInternacion.HasValue,
                MeIdInternacion = meIdInternacion
            };
        }

        // pa_MovEnc_VerificarSiEstaInternadoPorFichayFecha - sin cambios en la llamada
        private async Task<int?> VerificarSiEstaInternadoAsync(int fichaId, DateTime fecha, int hora)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<int?>(
                "pa_MovEnc_VerificarSiEstaInternadoPorFichayFecha",
                new { ficha_id = fichaId, fecha, hora },
                commandType: CommandType.StoredProcedure);
        }

        // panet_HistoriasClinicas_Insertar - sin cambios en la llamada. Usu_Modi/Fec_Modi
        // van null: es el alta, todavia no hubo ninguna edicion.
        private async Task<int> InsertarHistoriaClinicaAsync(AltaEvolucionRequestDto request, DateTime fecha, int hora)
        {
            using var conn = _connectionFactory.CreateConnection();
            var scopeIdentity = await conn.QueryFirstOrDefaultAsync<decimal>(
                "panet_HistoriasClinicas_Insertar",
                new
                {
                    Hc_Fecha = fecha,
                    Hc_Hs = hora,
                    Pre_id = request.PreId,
                    Ficha_id = request.FichaId,
                    Texto = request.Texto,
                    Usu_Alta = request.UsuAlta,
                    Fec_Alta = DateTime.Now,
                    Usu_Modi = (string?)null,
                    Fec_Modi = (DateTime?)null,
                    Me_id = request.MeId,
                    Esp_id = request.EspId,
                    np_id = request.NpId
                },
                commandType: CommandType.StoredProcedure);

            return (int)scopeIdentity;
        }

        // panet_LogHc_Insertar - sin cambios en la llamada. Los campos "*Anterior" van null
        // porque esta es la primera version de la nota (nada que loguear como "antes").
        private async Task<int?> InsertarLogHcAsync(AltaEvolucionRequestDto request, int hcId, DateTime fecha, int hora)
        {
            using var conn = _connectionFactory.CreateConnection();
            var scopeIdentity = await conn.QueryFirstOrDefaultAsync<decimal?>(
                "panet_LogHc_Insertar",
                new
                {
                    Hc_id = hcId,
                    Fecha_Log = fecha,
                    Hs_Log = hora,
                    Usu_Log = request.UsuAlta,
                    TextoAnterior = (string?)null,
                    TextoModi = request.Texto,
                    Accion = "Inserta",
                    Pre_idAnterior = (int?)null,
                    Pre_idModi = request.PreId,
                    Esp_idAnterior = (int?)null,
                    Esp_idModi = request.EspId,
                    Me_idAnterior = (int?)null,
                    Me_idModi = request.MeId,
                    Apd_id = (int?)null,
                    Ficha_id = request.FichaId
                },
                commandType: CommandType.StoredProcedure);

            return scopeIdentity.HasValue ? (int)scopeIdentity.Value : null;
        }

        // Epic 5: tab Estudios. pa_HistoriaClinica_TraerEstudios - sin cambios en la llamada,
        // sin orquestacion especial (1 sola llamada, de solo lectura).
        public async Task<IEnumerable<EstudioDto>> GetEstudiosAsync(int fichaId, DateTime fechaDesde, DateTime fechaHasta, string meArea)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<EstudioDto>(
                "pa_HistoriaClinica_TraerEstudios",
                new { Ficha_id = fichaId, FechaDesde = fechaDesde, FechaHasta = fechaHasta, Me_area = meArea },
                commandType: CommandType.StoredProcedure);
        }

        // dbo.Ficha no tiene un SP propio de busqueda por DNI (fic_nrodoc es char(9), con
        // ceros a la izquierda) - se resuelve el fichaId con una consulta directa en vez de
        // depender de la copia BH_FICHA_PACIENTES_BH (que es para datos de contacto del
        // pedido, no necesariamente esta al dia con GECLISA).
        public async Task<int?> GetFichaIdPorDniAsync(string dni)
        {
            var dniNormalizado = dni.Trim().PadLeft(9, '0');
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<int?>(
                "SELECT TOP 1 Ficha_id FROM dbo.Ficha WHERE fic_nrodoc = @dni",
                new { dni = dniNormalizado });
        }
    }
}
