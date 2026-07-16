using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos.BhDto;
using ValorModels.Models.BhModels;
using ValorModels.Models.UsersModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ValoresData.Commands.CmdSolPract
{
    public class SolPractBhCmd : ISolPractBhCmd
    {
        private readonly DataBaseContext _context;
        public SolPractBhCmd(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SolPractBhDto>> GetSolPractAsyncDistinct(
        DateTime? fechaCreacionRP = null,
        string? startFechaRP = null,
        string? endFechaRP = null,
        List<string>? unidad = null,
        string? dni = null,
        string? metodo = null,
        string? prestador = null,
        string? estudio = null,
        int? estadoPrograma = null,
        List<int>? estadoTurno = null,
        string? usuario = null,
        string? servicio = null,
        string? obrasocial = null,
        string? ultimoContacto = null,
        int? inductor = null
     )


        {
            _context.Database.SetCommandTimeout(60);

            var query =
                from v in _context.V_BEALTH_SOLPRAC
                join e in _context.REL_SOL_PRACT
                on new { IDPEDIDO = v.IDPEDIDO, IDESTUDIO = v.IDESTUDIO }
                equals new { IDPEDIDO = e.idPedido, IDESTUDIO = e.idEstudio }
                into joinedData
                from e in joinedData.DefaultIfEmpty()
                select new SolPractBhDto
                {
                    id = v.ID,
                    DNI = v.DNI,
                    NOMBRE = v.NOMBRE,
                    OBRASOCIAL = v.OBRASOCIAL,
                    PRESTADORQUEGENERASOLICITUD = v.PRESTADORQUEGENERASOLICITUD,
                    FECHA = v.FECHA,
                    idrelsol = e != null ? e.id : (int?)null,
                    idPedido = v.IDPEDIDO,
                    idEstudio = v.IDESTUDIO,
                    fechaGestion = e.fechaGestion,
                    observaciones = e.observaciones,
                    creado = e.creado,
                    usuario = e.usuario,
                    ESTUDIO = v.ESTUDIO,
                    turno_id = e.turno_id,
                    METODOOK = v.METODOOK,
                    unidad = v.UNIDAD,
                    servicio = v.SERVICIOSOLICITUD,
                    CONFESPECIAL = v.CONFESPECIAL,
                    INDUCTOR = v.INDUCTOR,
                    Estado_pedido = v.Estado_pedido,
                    ATENDIDO = v.ATENDIDO,
                    UNIDAD_NOMBRE = v.UNIDAD,
                    estado_Programa = v.estado_Programa,
                    tur_fecha = e.tur_fecha,
                    OSCOD = v.OSCOD,
                    INDUCTOR_ID = v.INDUCTOR_ID,
                    Estado_Turno_id = v.Estado_Turno_id
                    
                };

            // Aplicar filtros 
            if (!string.IsNullOrEmpty(startFechaRP) && !string.IsNullOrEmpty(endFechaRP))
            {
                if (DateTime.TryParseExact(startFechaRP, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate) &&
                    DateTime.TryParseExact(endFechaRP, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
                {
                    query = query.Where(v => v.FECHA >= startDate && v.FECHA <= endDate.Date);
                }
            }
            if (!string.IsNullOrEmpty(dni))
            {
                query = query.Where(v => v.DNI == dni);
            }

            if (!string.IsNullOrEmpty(metodo))
            {
                query = query.Where(v => v.METODOOK == metodo);
            }

            if (unidad != null && unidad.Any())
            {
                query = query.Where(v => unidad.Contains(v.UNIDAD_NOMBRE));
            }

            if (!string.IsNullOrEmpty(estudio))
            {
                query = query.Where(v => v.ESTUDIO == estudio);
            }

            if (estadoPrograma.HasValue)
            {
                query = query.Where(v => v.estado_Programa == estadoPrograma);
            }
            if (estadoTurno != null && estadoTurno.Any())
            {
                query = query.Where(v => estadoTurno.Contains(v.Estado_Turno_id ?? -1));
            }
            if (!string.IsNullOrEmpty(usuario))
            {
                query = query.Where(e => e.usuario == usuario);
            }
            if (!string.IsNullOrEmpty(servicio))
            {
                query = query.Where(v => v.servicio == servicio);
            }
            if (!string.IsNullOrEmpty(obrasocial))
            {
                query = query.Where(v => v.OSCOD == obrasocial);
            }
            if (!string.IsNullOrEmpty(ultimoContacto))
            {
                query = query.Where(e => e.fechaGestion == DateOnly.Parse(ultimoContacto));
            }
            if (inductor.HasValue)
            {
                query = query.Where(V => V.INDUCTOR_ID == inductor);
            }

            var resultados = await query.ToListAsync();

            var resultadosFiltrados = await query
            .OrderBy(v => v.FECHA)
            .GroupBy(v => new { v.DNI, v.unidad })
            .Select(g => g.First())
            .Take(100)
            .ToListAsync();


            return resultadosFiltrados;
        }

        public async Task<IEnumerable<SolPractBhDto>> GetPedidosAnterioresPorDniAsync(string dni)
        {
            // El DNI puede venir guardado con o sin ceros a la izquierda según el origen del dato
            // (turnero vs. solicitudes viejas/manuales). En vez de comparar por sufijo (LIKE '%valor',
            // que no puede usar índice y escanea toda la vista), armamos un set acotado de variantes
            // exactas (sin ceros, y con padding a 7/8/9 dígitos, los largos reales de DNI en la tabla)
            // y comparamos con IN, que sí puede resolverse con búsquedas puntuales.
            var dniSinCeros = string.IsNullOrEmpty(dni) ? dni : dni.TrimStart('0');
            var candidatosDni = new List<string> { dni, dniSinCeros };
            if (!string.IsNullOrEmpty(dniSinCeros))
            {
                candidatosDni.Add(dniSinCeros.PadLeft(7, '0'));
                candidatosDni.Add(dniSinCeros.PadLeft(8, '0'));
                candidatosDni.Add(dniSinCeros.PadLeft(9, '0'));
            }
            candidatosDni = candidatosDni.Distinct().ToList();

            // Usamos V_UNION_BEHEALTH_SOLPRACT (solo la unión de las dos tablas de pedidos) en vez de
            // V_BEALTH_SOLPRAC, que suma más de una decena de joins (turnos, inductores, seguimiento,
            // obra social, etc.) que no hacen falta para listar los pedidos anteriores de un paciente.
            var query =
                from v in _context.V_UNION_BEHEALTH_SOLPRACT
                where candidatosDni.Contains(v.DNI)
                orderby v.FECHA descending
                select new SolPractBhDto
                {
                    id = v.ID,
                    DNI = v.DNI,
                    NOMBRE = v.NOMBRE,
                    OBRASOCIAL = v.OBRASOCIAL,
                    PRESTADORQUEGENERASOLICITUD = v.PRESTADORQUEGENERASOLICITUD,
                    FECHA = v.FECHA ?? DateTime.MinValue,
                    idPedido = v.IDPEDIDO,
                    idEstudio = v.IDESTUDIO,
                    ESTUDIO = v.ESTUDIO,
                    METODOOK = v.METODOPRACTICA,
                    Estado_pedido = v.ESTADO,
                    DIAGNÓSTICO = v.DIAGNÓSTICO
                };

            return await query.Take(200).ToListAsync();
        }

        public async Task<IEnumerable<SolPractBhMetodoDto>> GetSolPractAsync(
                 DateTime? fechaCreacionRP = null,
                 string? startFechaRP = null,
                 string? endFechaRP = null,
                 List<string>? unidad = null,
                 string? dni = null,
                 string? metodo = null,
                 string? prestador = null,
                 string? estudio = null,
                 int? estadoPrograma = null,
                 List<int>? estadoTurno = null,
                 string? usuario = null,
                 string? servicio = null,
                 string? obrasocial = null,
                 string? ultimoContacto = null,
                 int? inductor = null
                )


        {

            //Crear la consulta base
            var query =
                from v in _context.V_BEALTH_SOLPRAC
                join e in _context.REL_SOL_PRACT
                on new { IDPEDIDO = v.IDPEDIDO, IDESTUDIO = v.IDESTUDIO }
                equals new { IDPEDIDO = e.idPedido, IDESTUDIO = e.idEstudio }
                into joinedData
                from e in joinedData.DefaultIfEmpty()
                select new SolPractBhDto
                {
                    id = v.ID,
                    DNI = v.DNI,
                    NOMBRE = v.NOMBRE,
                    OBRASOCIAL = v.OBRASOCIAL,
                    PRESTADORQUEGENERASOLICITUD = v.PRESTADORQUEGENERASOLICITUD,
                    FECHA = v.FECHA,
                    idrelsol = e != null ? e.id : (int?)null,
                    idPedido = v.IDPEDIDO,
                    idEstudio = v.IDESTUDIO,
                    fechaGestion = e.fechaGestion,
                    observaciones = e.observaciones,
                    creado = e.creado,
                    usuario = e.usuario,
                    ESTUDIO = v.ESTUDIO,
                    turno_id = e.turno_id,
                    METODOOK = v.METODOOK,
                    unidad = v.UNIDAD,
                    servicio = v.SERVICIOSOLICITUD,
                    CONFESPECIAL = v.CONFESPECIAL,
                    INDUCTOR = v.INDUCTOR,
                    Estado_pedido = v.Estado_pedido,
                    ATENDIDO = v.ATENDIDO,
                    UNIDAD_NOMBRE = v.UNIDAD,
                    estado_Programa = v.estado_Programa,
                    tur_fecha = e.tur_fecha,
                    OSCOD = v.OSCOD,
                    INDUCTOR_ID = v.INDUCTOR_ID,
                    Estado_Turno_id = v.Estado_Turno_id
                };

            // Aplicar filtros
            if (!string.IsNullOrEmpty(startFechaRP) && !string.IsNullOrEmpty(endFechaRP))
            {
                if (DateTime.TryParseExact(startFechaRP, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate) &&
                    DateTime.TryParseExact(endFechaRP, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
                {
                    query = query.Where(v => v.FECHA >= startDate && v.FECHA <= endDate);
                }
            }
            if (!string.IsNullOrEmpty(dni))
            {
                query = query.Where(v => v.DNI == dni);
            }

            if (!string.IsNullOrEmpty(metodo))
            {
                query = query.Where(v => v.METODOOK == metodo);
            }

            if (unidad != null && unidad.Any())
            {
                query = query.Where(v => unidad.Contains(v.UNIDAD_NOMBRE));
            }

            if (!string.IsNullOrEmpty(estudio))
            {
                query = query.Where(v => v.ESTUDIO == estudio);
            }

            if (estadoPrograma.HasValue)
            {
                query = query.Where(v => v.estado_Programa == estadoPrograma);
            }
            if (estadoTurno != null && estadoTurno.Any())
            {
                query = query.Where(v => estadoTurno.Contains(v.Estado_Turno_id ?? -1));
            }
            if (!string.IsNullOrEmpty(usuario))
            {
                query = query.Where(e => e.usuario == usuario);
            }
            if (!string.IsNullOrEmpty(servicio))
            {
                query = query.Where(v => v.servicio == servicio);
            }
            if (!string.IsNullOrEmpty(obrasocial))
            {
                query = query.Where(v => v.OSCOD == obrasocial);
            }
            if (!string.IsNullOrEmpty(ultimoContacto))
            {
                query = query.Where(e => e.fechaGestion == DateOnly.Parse(ultimoContacto));
            }
            if (inductor.HasValue)
            {
                query = query.Where(V => V.INDUCTOR_ID == inductor);
            }
            var allResults = await query.ToListAsync();

            // Aplicar lógica para "Laboratorio"
            var groupedResults = allResults
            .GroupBy(r => r.METODOOK)
            .Select(group => new SolPractBhMetodoDto
            {
                METODOOK = group.Key,
                FECHA = group.FirstOrDefault().FECHA ?? DateTime.MinValue,
                ESTUDIO = group.FirstOrDefault().ESTUDIO,
                tur_fecha = group.FirstOrDefault().tur_fecha,
                ATENDIDO = group.FirstOrDefault().ATENDIDO,
                PRESTADORQUEGENERASOLICITUD = group.FirstOrDefault().PRESTADORQUEGENERASOLICITUD,
                Estado_pedido = group.FirstOrDefault().Estado_pedido,
                CONFESPECIAL = group.FirstOrDefault().CONFESPECIAL,
                idPedido = group.FirstOrDefault().idPedido,
                INDUCTOR = group.FirstOrDefault().INDUCTOR,
                INDUCTOR_ID = group.FirstOrDefault().INDUCTOR_ID,
                estado_Programa = group.FirstOrDefault().estado_Programa,
                IDESTUDIO = group.FirstOrDefault().idEstudio,
                Estado_Turno_id = group.FirstOrDefault().Estado_Turno_id ?? 0,
                SolPractBhDtos = group.Key == "Laboratorio" ? group.Take(1) :
                 group.Where(x => x.turno_id != null).GroupBy(x => new { x.idEstudio, x.turno_id }).Select(g => g.First())
                 .Concat(group.Where(x => x.turno_id == null))
            });

            return groupedResults;
        }
        public async Task<IEnumerable<SolPractBhDto>> GetSolPractRpAsync(
   string? startFechaRP = null,
   string? endFechaRP = null,
   List<string>? unidad = null,
   string? dni = null,
   string? metodo = null,
   string? prestador = null,
   string? estudio = null,
   int? estadoPrograma = null,
   List<string>? estadoTurno = null,
   string? usuario = null,
   string? servicio = null,
   string? obrasocial = null,
   string? ultimoContacto = null,
   int? inductor = null,
   int? grupoGestionId = null
)
        {
            _context.Database.SetCommandTimeout(300);

            // Se arma el WHERE dinámico en SQL crudo: mismos filtros que antes, pero
            // ejecutados en una sola consulta (antes eran hasta 2 round-trips + dedupe en C#).
            var where = new SqlWhereBuilder();

            if (!string.IsNullOrEmpty(startFechaRP))
            {
                if (DateTime.TryParseExact(startFechaRP, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
                {
                    if (!string.IsNullOrEmpty(endFechaRP) &&
                        DateTime.TryParseExact(endFechaRP, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
                    {
                        where.Add("v.FECHA >= @startDate AND v.FECHA <= @endDate",
                            new SqlParameter("@startDate", startDate.Date),
                            new SqlParameter("@endDate", endDate.Date));
                    }
                    else
                    {
                        where.Add("v.FECHA = @startDate", new SqlParameter("@startDate", startDate.Date));
                    }
                }
            }

            if (!string.IsNullOrEmpty(dni))
                where.Add("v.DNI = @dni", new SqlParameter("@dni", dni));

            if (!string.IsNullOrEmpty(metodo))
                where.Add("v.METODOPRACTICA = @metodo", new SqlParameter("@metodo", metodo));

            if (unidad != null && unidad.Any())
                where.Add("v.UNIDAD IN (SELECT [value] FROM OPENJSON(@unidad) WITH ([value] nvarchar(max) '$'))",
                    new SqlParameter("@unidad", JsonSerializer.Serialize(unidad)));

            if (!string.IsNullOrEmpty(estudio))
                where.Add("v.ESTUDIO = @estudio", new SqlParameter("@estudio", estudio));

            if (estadoPrograma.HasValue)
                where.Add("v.estado_Programa = @estadoPrograma", new SqlParameter("@estadoPrograma", estadoPrograma.Value));

            if (estadoTurno != null && estadoTurno.Any())
                where.Add("v.seguimiento_estado_turno IN (SELECT [value] FROM OPENJSON(@estadoTurno) WITH ([value] nvarchar(max) '$'))",
                    new SqlParameter("@estadoTurno", JsonSerializer.Serialize(estadoTurno)));

            if (!string.IsNullOrEmpty(usuario))
                where.Add("v.seg_usuario_gestion = @usuario", new SqlParameter("@usuario", usuario));

            if (!string.IsNullOrEmpty(prestador))
                where.Add("v.PRESTADORQUEGENERASOLICITUD = @prestador", new SqlParameter("@prestador", prestador));

            if (!string.IsNullOrEmpty(obrasocial))
                where.Add("v.OSCOD = @obrasocial", new SqlParameter("@obrasocial", obrasocial));

            if (!string.IsNullOrEmpty(ultimoContacto) && DateOnly.TryParse(ultimoContacto, out var fechaContacto))
                where.Add("v.relsol_fechaGestion = @fechaContacto",
                    new SqlParameter("@fechaContacto", fechaContacto.ToDateTime(TimeOnly.MinValue)));

            if (inductor.HasValue)
                where.Add("v.INDUCTOR_ID = @inductor", new SqlParameter("@inductor", inductor.Value));

            if (!string.IsNullOrEmpty(servicio))
                where.Add("v.SERVICIOSOLICITUD = @servicio", new SqlParameter("@servicio", servicio));

            if (grupoGestionId.HasValue)
                where.Add("v.seg_grupoDeGestionId = @grupoGestionId", new SqlParameter("@grupoGestionId", grupoGestionId.Value));

            const string columnas = @"
        v.ID AS id,
        v.DNI,
        v.NOMBRE,
        v.OBRASOCIAL,
        v.PRESTADORQUEGENERASOLICITUD,
        v.FECHA,
        v.idrelsol,
        v.IDPEDIDO AS idPedido,
        v.IDESTUDIO AS idEstudio,
        v.relsol_fechaGestion AS fechaGestion,
        v.relsol_observaciones AS observaciones,
        v.relsol_creado AS creado,
        v.relsol_usuario AS usuario,
        v.ESTUDIO,
        v.turno_id,
        v.METODOPRACTICA,
        v.UNIDAD AS unidad,
        v.SERVICIOSOLICITUD AS servicio,
        v.CONFESPECIAL,
        v.INDUCTOR,
        v.Estado_pedido,
        v.ATENDIDO,
        v.UNIDAD AS UNIDAD_NOMBRE,
        v.estado_Programa,
        v.tur_fecha,
        v.OSCOD,
        v.INDUCTOR_ID,
        v.Estado_Turno_id,
        CAST(NULL AS nvarchar(max)) AS Estado_Turno,
        v.EMAIL,
        v.CELULAR,
        v.NUMEROAFILIADO,
        v.motivo_no_turno,
        v.seguimiento_estado_turno,
        v.seguimiento_cantidad_contactos,
        CAST(NULL AS nvarchar(max)) AS METODOOK,
        v.DIAGNÓSTICO,
        v.seg_usuario_gestion,
        v.grupo_gestion,
        v.seg_grupoDeGestionId,
        CAST(NULL AS nvarchar(max)) AS METODOOK2,
        v.SEG_OBSERVACION,
        v.OBSERVACION_INTERNA";

            string sql;

            // Caso 1: método específico distinto de Laboratorio -> el WHERE de arriba ya
            // excluye Laboratorio, no hay nada que deduplicar. Consulta simple sin ROW_NUMBER:
            // calcular esa ventana igual (aunque el resultado se descarte después) obliga a
            // ordenar/particionar todo el resultado sin necesidad, y es el caso más frecuente.
            if (!string.IsNullOrEmpty(metodo) && metodo != "Laboratorio")
            {
                sql = $@"
SELECT
    {columnas}
FROM V_BEALTH_SOLPRAC v
{where.BuildWhereClause()}
ORDER BY v.FECHA, v.DNI;";
            }
            else
            {
                // Caso 2 (metodo == Laboratorio) y Caso 3 (sin metodo): acá sí puede haber filas
                // de Laboratorio a deduplicar por pedido. Primera versión de este cambio calculaba
                // ROW_NUMBER() sobre TODO el resultado filtrado (Laboratorio + otros mezclados),
                // y eso resultó MÁS LENTO que el código viejo: la ventana obliga a SQL Server a
                // ordenar/particionar decenas de miles de filas de una, cuando en realidad solo
                // hace falta deduplicar el subconjunto de Laboratorio (bug de performance detectado
                // comparando contra producción con la misma consulta real antes de cerrar esto).
                //
                // Por eso acá se materializa la vista filtrada UNA sola vez en #Base (evita pagar
                // los ~15 JOIN dos veces, que es lo que hacía el código viejo con sus 2 consultas),
                // y el ROW_NUMBER() se calcula solo sobre el subconjunto ya materializado de
                // Laboratorio -particionado por idPedido, sin necesidad de incluir METODOPRACTICA
                // en la partición porque #Base.METODOPRACTICA ya es constante en ese subconjunto-,
                // mucho más chico y barato de ordenar. Medido contra la base real: ~8 segundos
                // totales vs ~17.5s con la ventana sobre todo el resultado y ~11-12s del código
                // viejo con sus 2 consultas separadas.
                sql = $@"
DROP TABLE IF EXISTS #Base;

SELECT
    {columnas}
INTO #Base
FROM V_BEALTH_SOLPRAC v
{where.BuildWhereClause()};

SELECT *
FROM (
    SELECT *, ROW_NUMBER() OVER (PARTITION BY idPedido ORDER BY (SELECT NULL)) AS rn
    FROM #Base
    WHERE METODOPRACTICA = 'Laboratorio'
) lab
WHERE rn = 1

UNION ALL

SELECT *, NULL AS rn
FROM #Base
WHERE ISNULL(METODOPRACTICA, '') <> 'Laboratorio'

ORDER BY FECHA, DNI;

DROP TABLE #Base;";
            }

            // FromSqlRaw contra un DbSet registrado (aunque sin tabla propia, ver DataBaseContext)
            // usa el shaper compilado de EF para materializar -- SqlQueryRaw<T> usa un mapeo
            // generico por reflexion, notablemente mas lento para un DTO con ~40 columnas y miles
            // de filas (medido: la diferencia explicaba varios segundos del tiempo total).
            return await _context.SolPractBhRpQuery
                .FromSqlRaw(sql, where.Parameters)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<SolPractBhRpDto>> GetSolPractRpPdfAsync(string IDPEDIDO, string metodo)
        {
            var query= await _context.V_BEALTH_SOLPRAC.Where(e => e.IDPEDIDO == IDPEDIDO && e.METODOOK == metodo).ToListAsync();

            string firma = null;
            if (query.Any())
            {
                int primerIdUsuario = int.Parse(query.First().CODIGOPRESTADOR);
                var firmaconsulta = await GetFirma(primerIdUsuario);
                var firmaUsuario = firmaconsulta.FirstOrDefault();

                if (firmaUsuario != null)
                {
                    firma = firmaUsuario.FIRMA;
                }
            }
            var result = query.Select(item => new SolPractBhRpDto
            {
                ID = item.ID,
                IDPEDIDO = item.IDPEDIDO,
                IDTRATAMIENTO = item.IDTRATAMIENTO,
                NOMBREBATERIA = item.NOMBREBATERIA,
                FECHA = item.FECHA,
                FECHACREACION = item.FECHACREACION,
                DIAGNÓSTICO = item.DIAGNÓSTICO,
                METODOOK = item.METODOOK,
                IDESTUDIO = item.IDESTUDIO,
                ESTUDIO = item.ESTUDIO,
                DNI = item.DNI.Trim(),
                NOMBRE = item.NOMBRE,
                IDOBRASOCIAL = item.IDOBRASOCIAL,
                OBRASOCIAL = item.OBRASOCIAL,
                NUMEROAFILIADO = item.NUMEROAFILIADO,
                CELULAR = item.CELULAR,
                EMAIL = item.EMAIL,
                CODIGOPRESTADOR = item.CODIGOPRESTADOR,
                PRESTADORQUEGENERASOLICITUD = item.PRESTADORQUEGENERASOLICITUD,
                IDTURNO = item.IDTURNO,
                FECHAHORA = item.FECHAHORA,
                FECHAHORAALTA = item.FECHAHORAALTA,
                FECHAHORAGESTIONDEESTADO = item.FECHAHORAGESTIONDEESTADO,
                USUARIOALTA = item.USUARIOALTA,
                IDSERVICIOTURNO = item.IDSERVICIOTURNO,
                SERVICIOTURNO = item.SERVICIOTURNO,
                IDSERVICIOSOLICITUD = item.IDSERVICIOSOLICITUD,
                SERVICIOSOLICITUD = item.SERVICIOSOLICITUD,
                CODIGOPRESTADORDELTURNO = item.CODIGOPRESTADORDELTURNO,
                PRESTADORDELTURNO = item.PRESTADORDELTURNO,
                FECHAHORACONF = item.FECHAHORACONF,
                FECHAHORAATENCION = item.FECHAHORAATENCION,
                ESTADO = item.ESTADO,
                USUARIOGESTIONOESTADO = item.USUARIOGESTIONOESTADO,
                CONTACTACION = item.CONTACTACION,
                MOTIVONOTURNO = item.MOTIVONOTURNO,
                OBSERVACIONES = item.OBSERVACIONES,
                CREADO = item.CREADO,
                USUARIO = item.USUARIO,
                UNIDAD = item.UNIDAD,
                UNIDAD_ID = item.UNIDAD_ID,
                CONFESPECIAL = item.CONFESPECIAL,
                Estado_pedido = item.Estado_pedido,
                ATENDIDO = item.ATENDIDO,
                INDUCTOR = item.INDUCTOR,
                estado_Programa = item.estado_Programa,
                OSCOD = item.OSCOD,
                INDUCTOR_ID = item.INDUCTOR_ID,
                Estado_Turno_id = item.Estado_Turno_id,
                No_gestion = item.No_gestion,
                motivo_no_turno = item.motivo_no_turno,
                METODOPRACTICA = item.METODOPRACTICA,
                // seguimiento_estado_turno=item.seguimiento_estado_turno,
                // seguimiento_cantidad_contactos=item.seguimiento_cantidad_contactos,
                // seg_usuario_gestion=item.seg_usuario_gestion,
                FIRMA = firma


            });
            return result;    
        }

        public async Task<IEnumerable<UserModel>> GetFirma(int User_id)
        {

            return await _context.BH_USERS.Where(e => e.ID == User_id).ToListAsync();

        }

        public async Task<bool> UpdateSolPractRpAsync(SolPractBhPedidoManualModel SolPract)
        {
            var update = await _context.BEALTH_SOLPRACT_P_MANUAL_OK.Where(e => e.IDPEDIDO == SolPract.IDPEDIDO).ToListAsync();
            if (update.Any())
            {
                foreach (var item in update)
                {
                    if (!string.IsNullOrEmpty(SolPract.IDOBRASOCIAL))
                        item.IDOBRASOCIAL = SolPract.IDOBRASOCIAL;

                    if (!string.IsNullOrEmpty(SolPract.OBRASOCIAL))
                        item.OBRASOCIAL = SolPract.OBRASOCIAL;

                    if (!string.IsNullOrEmpty(SolPract.EMAIL))
                        item.EMAIL = SolPract.EMAIL;

                    if (!string.IsNullOrEmpty(SolPract.NUMEROAFILIADO))
                        item.NUMEROAFILIADO = SolPract.NUMEROAFILIADO;


                    item.FECHA = SolPract.FECHA;


                }

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task<bool> UpdateSolPractOldRpAsync(SolPractBhPedidoManualModel SolPract)
        {
            var update = await _context.BEALTH_SOLPRACT.Where(e => e.IDPEDIDO == SolPract.IDPEDIDO).ToListAsync();
            if (update.Any())
            {
                foreach (var item in update)
                {
                    if (!string.IsNullOrEmpty(SolPract.IDOBRASOCIAL))
                        item.IDOBRASOCIAL = SolPract.IDOBRASOCIAL;

                    if (!string.IsNullOrEmpty(SolPract.OBRASOCIAL))
                        item.OBRASOCIAL = SolPract.OBRASOCIAL;

                    if (!string.IsNullOrEmpty(SolPract.EMAIL))
                        item.EMAIL = SolPract.EMAIL;

                    if (!string.IsNullOrEmpty(SolPract.NUMEROAFILIADO))
                        item.NUMEROAFILIADO = SolPract.NUMEROAFILIADO;


                    item.FECHA = SolPract.FECHA;


                }

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task<UnionSolPractModel> GetSolPractUnionAsync(string IDPEDIDO)
        {
            return await _context.V_UNION_BEHEALTH_SOLPRACT.Where(e=> e.IDPEDIDO == IDPEDIDO).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<SolPractBhMetodoDto>> GetSolPractSeguimientoAsync(
               DateTime? fechaCreacionRP = null,
               string? startFechaRP = null,
               string? endFechaRP = null,
               List<string>? unidad = null,
               string? dni = null,
               string? metodo = null,
               string? prestador = null,
               string? estudio = null,
               int? estadoPrograma = null,
               List<int>? estadoTurno = null,
               string? usuario = null,
               string? servicio = null,
               string? obrasocial = null,
               string? ultimoContacto = null,
               int? inductor = null
              )


        {
            _context.Database.SetCommandTimeout(60);
            //Crear la consulta base
            var query =
                from v in _context.V_BEALTH_SOLPRAC
                join e in _context.REL_SOL_PRACT
                on new { IDPEDIDO = v.IDPEDIDO, IDESTUDIO = v.IDESTUDIO }
                equals new { IDPEDIDO = e.idPedido, IDESTUDIO = e.idEstudio }
                into joinedData
                from e in joinedData.DefaultIfEmpty()
                select new SolPractBhDto
                {
                    id = v.ID,
                    DNI = v.DNI,
                    NOMBRE = v.NOMBRE,
                    OBRASOCIAL = v.OBRASOCIAL,
                    PRESTADORQUEGENERASOLICITUD = v.PRESTADORQUEGENERASOLICITUD,
                    FECHA = v.FECHA,
                    idrelsol = e != null ? e.id : (int?)null,
                    idPedido = v.IDPEDIDO,
                    idEstudio = v.IDESTUDIO,
                    fechaGestion = e.fechaGestion,
                    observaciones = e.observaciones,
                    creado = e.creado,
                    usuario = e.usuario,
                    ESTUDIO = v.ESTUDIO,
                    turno_id = e.turno_id,
                    METODOOK = v.METODOOK,
                    unidad = v.UNIDAD,
                    servicio = v.SERVICIOSOLICITUD,
                    CONFESPECIAL = v.CONFESPECIAL,
                    INDUCTOR = v.INDUCTOR,
                    Estado_pedido = v.Estado_pedido,
                    ATENDIDO = v.ATENDIDO,
                    UNIDAD_NOMBRE = v.UNIDAD,
                    estado_Programa = v.estado_Programa,
                    tur_fecha = e.tur_fecha,
                    OSCOD = v.OSCOD,
                    INDUCTOR_ID = v.INDUCTOR_ID,
                    Estado_Turno_id = v.Estado_Turno_id,
                    METODOOK2 = v.METODOOK2

                };

            // Aplicar filtros
            if (!string.IsNullOrEmpty(startFechaRP) && !string.IsNullOrEmpty(endFechaRP))
            {
                if (DateTime.TryParseExact(startFechaRP, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate) &&
                    DateTime.TryParseExact(endFechaRP, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
                {
                    query = query.Where(v => v.FECHA >= startDate && v.FECHA <= endDate);
                }
            }
            if (!string.IsNullOrEmpty(dni))
            {
                query = query.Where(v => v.DNI == dni);
            }

            if (!string.IsNullOrEmpty(metodo))
            {
                query = query.Where(v => v.METODOOK == metodo);
            }

            if (unidad != null && unidad.Any())
            {
                query = query.Where(v => unidad.Contains(v.UNIDAD_NOMBRE));
            }

            if (!string.IsNullOrEmpty(estudio))
            {
                query = query.Where(v => v.ESTUDIO == estudio);
            }

            if (estadoPrograma.HasValue)
            {
                query = query.Where(v => v.estado_Programa == estadoPrograma);
            }
            if (estadoTurno != null && estadoTurno.Any())
            {
                query = query.Where(v => estadoTurno.Contains(v.Estado_Turno_id ?? -1));
            }
            if (!string.IsNullOrEmpty(usuario))
            {
                query = query.Where(e => e.usuario == usuario);
            }
            if (!string.IsNullOrEmpty(servicio))
            {
                query = query.Where(v => v.servicio == servicio);
            }
            if (!string.IsNullOrEmpty(obrasocial))
            {
                query = query.Where(v => v.OSCOD == obrasocial);
            }
            if (!string.IsNullOrEmpty(ultimoContacto))
            {
                query = query.Where(e => e.fechaGestion == DateOnly.Parse(ultimoContacto));
            }
            if (inductor.HasValue)
            {
                query = query.Where(V => V.INDUCTOR_ID == inductor);
            }
            var allResults = await query.ToListAsync();

            // Aplicar lógica para "Laboratorio"
            var groupedResults = allResults
            .GroupBy(r => r.METODOOK2)
            .Select(group => new SolPractBhMetodoDto
            {
                METODOOK2 = group.Key,
                METODOOK=group.FirstOrDefault().METODOOK,
                FECHA = group.FirstOrDefault().FECHA ?? DateTime.MinValue,
                ESTUDIO = group.FirstOrDefault().ESTUDIO,
                tur_fecha = group.FirstOrDefault().tur_fecha,
                ATENDIDO = group.FirstOrDefault().ATENDIDO,
                PRESTADORQUEGENERASOLICITUD = group.FirstOrDefault().PRESTADORQUEGENERASOLICITUD,
                Estado_pedido = group.FirstOrDefault().Estado_pedido,
                CONFESPECIAL = group.FirstOrDefault().CONFESPECIAL,
                idPedido = group.FirstOrDefault().idPedido,
                INDUCTOR = group.FirstOrDefault().INDUCTOR,
                INDUCTOR_ID = group.FirstOrDefault().INDUCTOR_ID,
                estado_Programa = group.FirstOrDefault().estado_Programa,
                IDESTUDIO = group.FirstOrDefault().idEstudio,
                Estado_Turno_id = group.FirstOrDefault().Estado_Turno_id ?? 0,
                SolPractBhDtos = group.Key == "Laboratorio" ? group.Take(1) :
                 group.Where(x => x.turno_id != null).GroupBy(x => new { x.idEstudio, x.turno_id }).Select(g => g.First())
                 .Concat(group.Where(x => x.turno_id == null))
            });

            return groupedResults;
        }

        public async Task<bool> DeleteSolPractTotalAsync(string idpedido)
        {
            var rp = await _context.BEALTH_SOLPRACT_P_MANUAL_OK.Where(e => e.IDPEDIDO == idpedido).ToListAsync();
            if (rp == null || !rp.Any())
            {
                return false;
            }
            _context.BEALTH_SOLPRACT_P_MANUAL_OK.RemoveRange(rp);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<string>> GetDnisConRpAsync(List<string> dnis)
        {
            // Mismo criterio que GetPedidosAnterioresPorDniAsync: el DNI puede estar guardado con o
            // sin ceros a la izquierda segun el origen del dato, asi que expandimos cada uno a sus
            // variantes y comparamos todo junto con IN (una sola consulta para toda la lista).
            var candidatoAOriginal = new Dictionary<string, string>();
            foreach (var dni in dnis.Where(d => !string.IsNullOrEmpty(d)).Distinct())
            {
                var dniSinCeros = dni.TrimStart('0');
                var variantes = new List<string> { dni, dniSinCeros, dniSinCeros.PadLeft(7, '0'), dniSinCeros.PadLeft(8, '0'), dniSinCeros.PadLeft(9, '0') };
                foreach (var variante in variantes.Distinct())
                {
                    candidatoAOriginal[variante] = dni;
                }
            }

            var candidatos = candidatoAOriginal.Keys.ToList();

            var dnisEncontrados = await _context.BEALTH_SOLPRACT_P_MANUAL_OK
                .Where(b => candidatos.Contains(b.DNI))
                .Select(b => b.DNI)
                .Distinct()
                .ToListAsync();

            return dnisEncontrados
                .Select(d => candidatoAOriginal.TryGetValue(d, out var original) ? original : d)
                .Distinct();
        }
    }
  }

