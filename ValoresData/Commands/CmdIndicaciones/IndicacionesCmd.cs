using System.Collections.Concurrent;
using System.Data;
using Dapper;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos.HistoriasClinicasDto;
using ValorModels.Dtos.IndicacionesDto;

namespace ValoresData.Commands.CmdIndicaciones
{
    // Epic 3: tab Indicaciones y Recetas. Agrupa las 7 llamadas de listado de la Fase 3
    // bajo un solo endpoint (en paralelo, sin duplicar pa_AteListarPorRangoFechaFichaArea
    // como hacia el cliente actual), y resuelve el enriquecimiento Prestador/Usuario/
    // Especialidad de las indicaciones con dedupe + concurrencia limitada - mismo patron
    // N+1 que Laboratorio pero a escala chica (ver Epic 2 / LaboratorioCmd).
    public class IndicacionesCmd : IIndicacionesCmd
    {
        private readonly IGeclisaConnectionFactory _connectionFactory;

        public IndicacionesCmd(IGeclisaConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<CargaInicialIndicacionesDto> GetCargaInicialAsync(
            int fichaId, DateTime fechaDesde, DateTime fechaHasta, string area,
            int? usuarioIdQueConsulta, string confCod, int maxConcurrencia)
        {
            var recetasTask = GetRecetasAsync(fechaDesde, fechaHasta, fichaId);
            var atencionesTask = GetAtencionesAsync(fechaDesde, fechaHasta, fichaId, area);
            var indicacionesTask = GetIndicacionesAsync(fichaId);
            var pedidosTask = GetPedidosEstudiosAsync(fichaId, fechaDesde, fechaHasta, area);
            var configTask = GetConfigAsync(confCod);
            var archivosTask = GetArchivosAsync(fichaId, fechaDesde, fechaHasta);
            var prestadoresPermitidosTask = usuarioIdQueConsulta.HasValue
                ? GetPrestadoresPermitidosAsync(usuarioIdQueConsulta.Value)
                : Task.FromResult(Enumerable.Empty<PrestadorDto>());

            await Task.WhenAll(recetasTask, atencionesTask, indicacionesTask, pedidosTask, configTask, archivosTask, prestadoresPermitidosTask);

            var indicaciones = (await indicacionesTask).ToList();

            var (prestadoresPorId, usuariosPorNombre, especialidadesPorId) =
                await EnriquecerIndicacionesAsync(indicaciones, maxConcurrencia);

            return new CargaInicialIndicacionesDto
            {
                Recetas = (await recetasTask).ToList(),
                Atenciones = (await atencionesTask).ToList(),
                Indicaciones = indicaciones,
                PedidosEstudios = (await pedidosTask).ToList(),
                Config = await configTask,
                Archivos = (await archivosTask).ToList(),
                PrestadoresPermitidos = (await prestadoresPermitidosTask).ToList(),
                PrestadoresPorId = prestadoresPorId,
                UsuariosPorNombre = usuariosPorNombre,
                EspecialidadesPorId = especialidadesPorId
            };
        }

        // Dedupe pre_id/usu_alta/esp_id de las indicaciones y resuelve cada set distinto en
        // paralelo con concurrencia limitada. Fail-soft: un id que falla simplemente no
        // aparece en el diccionario resultante.
        private async Task<(Dictionary<int, PrestadorDto>, Dictionary<string, UsuarioGeclisaDto>, Dictionary<int, EspecialidadDto>)>
            EnriquecerIndicacionesAsync(List<IndicacionMedicaDto> indicaciones, int maxConcurrencia)
        {
            var preIds = indicaciones.Where(i => i.pre_id.HasValue).Select(i => i.pre_id!.Value).Distinct().ToList();
            var usuarioNoms = indicaciones.Where(i => !string.IsNullOrWhiteSpace(i.usu_alta)).Select(i => i.usu_alta!).Distinct().ToList();
            var espIds = indicaciones.Where(i => i.esp_id.HasValue).Select(i => i.esp_id!.Value).Distinct().ToList();

            var prestadores = new ConcurrentDictionary<int, PrestadorDto>();
            var usuarios = new ConcurrentDictionary<string, UsuarioGeclisaDto>();
            var especialidades = new ConcurrentDictionary<int, EspecialidadDto>();

            using var throttle = new SemaphoreSlim(maxConcurrencia);

            var tareasPrestadores = preIds.Select(async preId =>
            {
                await throttle.WaitAsync();
                try
                {
                    var prestador = await GetPrestadorPorIdAsync(preId);
                    if (prestador != null) prestadores[preId] = prestador;
                }
                catch (Exception) { /* fail-soft */ }
                finally { throttle.Release(); }
            });

            var tareasUsuarios = usuarioNoms.Select(async usuarioNom =>
            {
                await throttle.WaitAsync();
                try
                {
                    var usuario = await GetUsuarioPorNombreAsync(usuarioNom);
                    if (usuario != null) usuarios[usuarioNom] = usuario;
                }
                catch (Exception) { /* fail-soft */ }
                finally { throttle.Release(); }
            });

            var tareasEspecialidades = espIds.Select(async espId =>
            {
                await throttle.WaitAsync();
                try
                {
                    var especialidad = await GetEspecialidadPorIdAsync(espId);
                    if (especialidad != null) especialidades[espId] = especialidad;
                }
                catch (Exception) { /* fail-soft */ }
                finally { throttle.Release(); }
            });

            await Task.WhenAll(tareasPrestadores.Concat(tareasUsuarios).Concat(tareasEspecialidades));

            return (new Dictionary<int, PrestadorDto>(prestadores),
                    new Dictionary<string, UsuarioGeclisaDto>(usuarios),
                    new Dictionary<int, EspecialidadDto>(especialidades));
        }

        // panet_HistoriaClinica_ListaReceta - sin cambios en la llamada
        private async Task<IEnumerable<RecetaListadoDto>> GetRecetasAsync(DateTime fechaDesde, DateTime fechaHasta, int fichaId)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<RecetaListadoDto>(
                "panet_HistoriaClinica_ListaReceta",
                new { FechaDesde = fechaDesde, FechaHasta = fechaHasta, Ficha_id = fichaId, pre_id = 0 },
                commandType: CommandType.StoredProcedure);
        }

        // pa_AteListarPorRangoFechaFichaArea - sin cambios en la llamada, se invoca 1 sola vez
        // (en la traza original se pedia duplicada - eso no se replica)
        private async Task<IEnumerable<AtePorRangoDto>> GetAtencionesAsync(DateTime fechaInicio, DateTime fechaFin, int fichaId, string area)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<AtePorRangoDto>(
                "pa_AteListarPorRangoFechaFichaArea",
                new { FechaInicio = fechaInicio, FechaFin = fechaFin, Ficha_id = fichaId, Area = area },
                commandType: CommandType.StoredProcedure);
        }

        // panet_IndicacionesMedicas_ListarPorFicha - sin cambios en la llamada
        private async Task<IEnumerable<IndicacionMedicaDto>> GetIndicacionesAsync(int fichaId)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<IndicacionMedicaDto>(
                "panet_IndicacionesMedicas_ListarPorFicha",
                new { Ficha_id = fichaId },
                commandType: CommandType.StoredProcedure);
        }

        // panet_PedidosEstudiosEnca_ConsultarPorMeId_o_FichaId - sin cambios en la llamada.
        // Me_id=0 fuerza al SP a filtrar por Ficha_id (ver logica del propio SP).
        private async Task<IEnumerable<PedidoEstudioEncaDto>> GetPedidosEstudiosAsync(int fichaId, DateTime fechaDesde, DateTime fechaHasta, string area)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<PedidoEstudioEncaDto>(
                "panet_PedidosEstudiosEnca_ConsultarPorMeId_o_FichaId",
                new { Me_id = 0, Ficha_id = fichaId, fechaDesde, fechaHasta, area },
                commandType: CommandType.StoredProcedure);
        }

        // panet_ConfigPestaniasHistoriaClinicaInternado_ObtenerPorCodigo - sin cambios en la llamada
        private async Task<ConfigPestaniaDto?> GetConfigAsync(string confCod)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<ConfigPestaniaDto>(
                "panet_ConfigPestaniasHistoriaClinicaInternado_ObtenerPorCodigo",
                new { confCod },
                commandType: CommandType.StoredProcedure);
        }

        // panet_HistoriaClinicaArchivo_BuscarPorFechaFichaPreId - sin cambios en la llamada
        private async Task<IEnumerable<HistoriaClinicaArchivoDto>> GetArchivosAsync(int fichaId, DateTime fechaInicio, DateTime fechaFin)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<HistoriaClinicaArchivoDto>(
                "panet_HistoriaClinicaArchivo_BuscarPorFechaFichaPreId",
                new { fichaId, fechaInicio, fechaFin, preId = (int?)null },
                commandType: CommandType.StoredProcedure);
        }

        // panet_Prestadores_BuscarPorPermisosUsuarioLogeado - sin cambios en la llamada
        private async Task<IEnumerable<PrestadorDto>> GetPrestadoresPermitidosAsync(int usuarioId)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<PrestadorDto>(
                "panet_Prestadores_BuscarPorPermisosUsuarioLogeado",
                new { usuarioId },
                commandType: CommandType.StoredProcedure);
        }

        // panet_Prestadores_BuscarPorID - sin cambios en la llamada
        private async Task<PrestadorDto?> GetPrestadorPorIdAsync(int preId)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<PrestadorDto>(
                "panet_Prestadores_BuscarPorID",
                new { pre_id = preId },
                commandType: CommandType.StoredProcedure);
        }

        // panet_Usuarios_BuscarPorNombreUsuario - sin cambios en la llamada
        private async Task<UsuarioGeclisaDto?> GetUsuarioPorNombreAsync(string usuarioNom)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<UsuarioGeclisaDto>(
                "panet_Usuarios_BuscarPorNombreUsuario",
                new { usuario_nom = usuarioNom },
                commandType: CommandType.StoredProcedure);
        }

        // panet_Especialidades_BuscarPorID - sin cambios en la llamada
        private async Task<EspecialidadDto?> GetEspecialidadPorIdAsync(int espId)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<EspecialidadDto>(
                "panet_Especialidades_BuscarPorID",
                new { esp_id = espId },
                commandType: CommandType.StoredProcedure);
        }

        // Epic 6: detalle de receta. panet_HistoriaClinica_DetalleReceta - sin cambios en la
        // llamada. En la traza original este SP se llamaba 2 veces seguidas con el mismo
        // RecEnca_id (bug de doble disparo del frontend) - acá se expone una sola vez por
        // llamada; que no se dispare 2 veces por click es responsabilidad del cliente nuevo.
        public async Task<IEnumerable<RecetaItemDto>> GetDetalleRecetaAsync(int recEncaId)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<RecetaItemDto>(
                "panet_HistoriaClinica_DetalleReceta",
                new { RecEnca_id = recEncaId },
                commandType: CommandType.StoredProcedure);
        }
    }
}
