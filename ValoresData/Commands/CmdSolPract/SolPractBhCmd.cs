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
                select new
                {
                    Dto = new SolPractBhDto
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
                        METODOPRACTICA = v.METODOPRACTICA,
                        Estado_pedido = v.ESTADO,
                        DIAGNÓSTICO = v.DIAGNÓSTICO
                    },
                    v.CODIGOPRESTADOR
                };

            var resultado = await query.Take(200).ToListAsync();

            // CODIGOPRESTADOR es el ID (como string) de BH_USERS, no la matricula en si -misma
            // logica que ya usa GetFirma mas abajo-.
            var idsPrestador = resultado
                .Select(r => int.TryParse(r.CODIGOPRESTADOR, out var id) ? id : (int?)null)
                .Where(id => id.HasValue)
                .Select(id => id!.Value)
                .Distinct()
                .ToList();

            if (idsPrestador.Any())
            {
                var matriculas = await _context.BH_USERS
                    .Where(u => idsPrestador.Contains(u.ID))
                    .ToDictionaryAsync(u => u.ID, u => u.Matricula);

                foreach (var r in resultado)
                {
                    if (int.TryParse(r.CODIGOPRESTADOR, out var id) && matriculas.TryGetValue(id, out var matricula))
                    {
                        r.Dto.MATRICULA = matricula;
                    }
                }
            }

            return resultado.Select(r => r.Dto);
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

            // Caso 1 (metodo especifico no-Laboratorio) sigue armando un unico WHERE contra
            // V_BEALTH_SOLPRAC (v.), igual que antes. Caso 2/3 arma DOS: "barato" (contra la
            // vista liviana V_BEALTH_SOLPRAC_FILTRO, lv.) y "caro" (contra V_BEALTH_SOLPRAC
            // completa, v., solo para lo que de verdad necesita sus joins). Ver comentario mas
            // abajo, junto al SQL de Caso 2/3, para el detalle de por que existe esta separacion.
            bool esCasoSimple = !string.IsNullOrEmpty(metodo) && metodo != "Laboratorio";
            var where = new SqlWhereBuilder();
            var whereBarato = new SqlWhereBuilder();
            var whereCaro = new SqlWhereBuilder();

            void AgregarBarato(string condicionLv, string condicionV, params SqlParameter[] parametros)
            {
                if (esCasoSimple) where.Add(condicionV, parametros);
                else whereBarato.Add(condicionLv, parametros);
            }

            if (!string.IsNullOrEmpty(startFechaRP))
            {
                if (DateTime.TryParseExact(startFechaRP, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
                {
                    if (!string.IsNullOrEmpty(endFechaRP) &&
                        DateTime.TryParseExact(endFechaRP, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
                    {
                        AgregarBarato("lv.FECHA >= @startDate AND lv.FECHA <= @endDate", "v.FECHA >= @startDate AND v.FECHA <= @endDate",
                            new SqlParameter("@startDate", startDate.Date),
                            new SqlParameter("@endDate", endDate.Date));
                    }
                    else
                    {
                        AgregarBarato("lv.FECHA = @startDate", "v.FECHA = @startDate", new SqlParameter("@startDate", startDate.Date));
                    }
                }
            }

            if (!string.IsNullOrEmpty(dni))
                AgregarBarato("lv.DNI = @dni", "v.DNI = @dni", new SqlParameter("@dni", dni));

            if (!string.IsNullOrEmpty(metodo))
                AgregarBarato("lv.METODOPRACTICA = @metodo", "v.METODOPRACTICA = @metodo", new SqlParameter("@metodo", metodo));

            // UNIDAD depende de v_unidadmedicadet (linked server): siempre "cara", incluso en Caso 2/3.
            if (unidad != null && unidad.Any())
            {
                var pUnidad = new SqlParameter("@unidad", JsonSerializer.Serialize(unidad));
                if (esCasoSimple) where.Add("v.UNIDAD IN (SELECT [value] FROM OPENJSON(@unidad) WITH ([value] nvarchar(max) '$'))", pUnidad);
                else whereCaro.Add("v.UNIDAD IN (SELECT [value] FROM OPENJSON(@unidad) WITH ([value] nvarchar(max) '$'))", pUnidad);
            }

            if (!string.IsNullOrEmpty(estudio))
                AgregarBarato("lv.ESTUDIO = @estudio", "v.ESTUDIO = @estudio", new SqlParameter("@estudio", estudio));

            // estado_Programa sale de ASIGNACION_ESTADO_PROGRAMA, que a su vez cruza por
            // s.UNIDAD (v_unidadmedicadet): tambien depende del linked server, va en "cara".
            if (estadoPrograma.HasValue)
            {
                var pEstadoPrograma = new SqlParameter("@estadoPrograma", estadoPrograma.Value);
                if (esCasoSimple) where.Add("v.estado_Programa = @estadoPrograma", pEstadoPrograma);
                else whereCaro.Add("v.estado_Programa = @estadoPrograma", pEstadoPrograma);
            }

            if (estadoTurno != null && estadoTurno.Any())
            {
                var pEstadoTurno = new SqlParameter("@estadoTurno", JsonSerializer.Serialize(estadoTurno));
                AgregarBarato(
                    "lv.seguimiento_estado_turno IN (SELECT [value] FROM OPENJSON(@estadoTurno) WITH ([value] nvarchar(max) '$'))",
                    "v.seguimiento_estado_turno IN (SELECT [value] FROM OPENJSON(@estadoTurno) WITH ([value] nvarchar(max) '$'))",
                    pEstadoTurno);
            }

            if (!string.IsNullOrEmpty(usuario))
                AgregarBarato("lv.seg_usuario_gestion = @usuario", "v.seg_usuario_gestion = @usuario", new SqlParameter("@usuario", usuario));

            if (!string.IsNullOrEmpty(prestador))
                AgregarBarato("lv.PRESTADORQUEGENERASOLICITUD = @prestador", "v.PRESTADORQUEGENERASOLICITUD = @prestador", new SqlParameter("@prestador", prestador));

            if (!string.IsNullOrEmpty(obrasocial))
                AgregarBarato("lv.OSCOD = @obrasocial", "v.OSCOD = @obrasocial", new SqlParameter("@obrasocial", obrasocial));

            if (!string.IsNullOrEmpty(ultimoContacto) && DateOnly.TryParse(ultimoContacto, out var fechaContacto))
            {
                var pFechaContacto = new SqlParameter("@fechaContacto", fechaContacto.ToDateTime(TimeOnly.MinValue));
                AgregarBarato("lv.relsol_fechaGestion = @fechaContacto", "v.relsol_fechaGestion = @fechaContacto", pFechaContacto);
            }

            // INDUCTOR_ID sale de ASIGNACION_INDUCTORES, que cruza por x.UNIDAD = s.UNIDAD
            // (v_unidadmedicadet): depende del linked server, va en "cara".
            if (inductor.HasValue)
            {
                var pInductor = new SqlParameter("@inductor", inductor.Value);
                if (esCasoSimple) where.Add("v.INDUCTOR_ID = @inductor", pInductor);
                else whereCaro.Add("v.INDUCTOR_ID = @inductor", pInductor);
            }

            if (!string.IsNullOrEmpty(servicio))
                AgregarBarato("lv.SERVICIOSOLICITUD = @servicio", "v.SERVICIOSOLICITUD = @servicio", new SqlParameter("@servicio", servicio));

            if (grupoGestionId.HasValue)
                AgregarBarato("lv.seg_grupoDeGestionId = @grupoGestionId", "v.seg_grupoDeGestionId = @grupoGestionId", new SqlParameter("@grupoGestionId", grupoGestionId.Value));

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
            object[] parametros;

            // Caso 1: método específico distinto de Laboratorio -> el WHERE de arriba ya
            // excluye Laboratorio, no hay nada que deduplicar. Consulta simple sin ROW_NUMBER:
            // calcular esa ventana igual (aunque el resultado se descarte después) obliga a
            // ordenar/particionar todo el resultado sin necesidad, y es el caso más frecuente.
            if (esCasoSimple)
            {
                sql = $@"
SELECT
    {columnas}
FROM V_BEALTH_SOLPRAC v
{where.BuildWhereClause()}
ORDER BY v.FECHA, v.DNI;";
                parametros = where.Parameters;
            }
            else
            {
                // Caso 2 (metodo == Laboratorio) y Caso 3 (sin metodo): acá sí puede haber filas
                // de Laboratorio a deduplicar por pedido, y son los casos con los filtros más
                // amplios (sin metodo/unidad seguido). Filtrar directo contra V_BEALTH_SOLPRAC
                // obliga a evaluar sus ~15 joins -1 de ellos contra linked server (v_unidadmedicadet),
                // y otros 2 que dependen de esa misma tabla (estado_Programa, INDUCTOR_ID)- para
                // TODAS las filas candidatas del rango de fechas, aunque el filtro real (ej.
                // estadoTurno+grupoGestionId) termine dejando solo un puñado. Medido contra
                // producción: una consulta así con ~14 mil filas candidatas tardaba 40-47s.
                //
                // Por eso se separa en dos fases:
                //  1) "whereBarato" resuelve que (idPedido, idEstudio) matchean el filtro contra
                //     V_BEALTH_SOLPRAC_FILTRO, la vista liviana sin esos 3 joins caros (ver su
                //     definición en SQL Server). Acá también se hace el dedup de Laboratorio,
                //     porque ya tenemos METODOPRACTICA disponible sin pagar nada extra.
                //  2) Recién ahí se enriquece contra V_BEALTH_SOLPRAC completa, pero SOLO para
                //     esas filas (join a #Claves) -y "whereCaro" (unidad/estadoPrograma/inductor)
                //     se aplica acá, que es donde esas columnas existen-.
                // Medido contra producción con el mismo filtro real: ~5.5s totales (fase 1 ~2.6s
                // + fase 2 ~2.9s) vs los 40-47s de antes.
                sql = $@"
DROP TABLE IF EXISTS #Base;

SELECT lv.IDPEDIDO, lv.IDESTUDIO_NUM, lv.METODOPRACTICA
INTO #Base
FROM V_BEALTH_SOLPRAC_FILTRO lv
{whereBarato.BuildWhereClause()};

DROP TABLE IF EXISTS #Claves;

SELECT IDPEDIDO, IDESTUDIO_NUM
INTO #Claves
FROM (
    SELECT IDPEDIDO, IDESTUDIO_NUM, ROW_NUMBER() OVER (PARTITION BY IDPEDIDO ORDER BY (SELECT NULL)) AS rn
    FROM #Base
    WHERE METODOPRACTICA = 'Laboratorio'
) lab
WHERE rn = 1

UNION ALL

SELECT IDPEDIDO, IDESTUDIO_NUM
FROM #Base
WHERE ISNULL(METODOPRACTICA, '') <> 'Laboratorio';

SELECT
    {columnas}
FROM V_BEALTH_SOLPRAC v
INNER JOIN #Claves c ON v.IDPEDIDO = c.IDPEDIDO AND TRY_CAST(v.IDESTUDIO AS INT) = c.IDESTUDIO_NUM
{whereCaro.BuildWhereClause()}
ORDER BY v.FECHA, v.DNI;

DROP TABLE #Base;
DROP TABLE #Claves;";
                parametros = whereBarato.Parameters.Concat(whereCaro.Parameters).ToArray();
            }

            // FromSqlRaw contra un DbSet registrado (aunque sin tabla propia, ver DataBaseContext)
            // usa el shaper compilado de EF para materializar -- SqlQueryRaw<T> usa un mapeo
            // generico por reflexion, notablemente mas lento para un DTO con ~40 columnas y miles
            // de filas (medido: la diferencia explicaba varios segundos del tiempo total).
            return await _context.SolPractBhRpQuery
                .FromSqlRaw(sql, parametros)
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

                    if (!string.IsNullOrEmpty(SolPract.CELULAR))
                        item.CELULAR = SolPract.CELULAR;


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

