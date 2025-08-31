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
                query = query.Where(v => estadoTurno.Contains(v.Estado_Turno_id));
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
                query = query.Where(v => estadoTurno.Contains(v.Estado_Turno_id));
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
                FECHA = group.FirstOrDefault().FECHA,
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
                Estado_Turno_id = group.FirstOrDefault().Estado_Turno_id,
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
                    int? estadoTurno = null,
                    string? usuario = null,
                    string? servicio = null,
                    string? obrasocial = null,
                    string? ultimoContacto = null,
                    int? inductor = null)
        {

            var baseQuery =
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
                    EMAIL = v.EMAIL,
                    CELULAR = v.CELULAR,
                    NUMEROAFILIADO = v.NUMEROAFILIADO,
                    motivo_no_turno = v.motivo_no_turno,
                    seguimiento_estado_turno = v.seguimiento_estado_turno,
                    seguimiento_cantidad_contactos =v.seguimiento_cantidad_contactos,
                    DIAGNÓSTICO=v.DIAGNÓSTICO

                };

    
            if (!string.IsNullOrEmpty(startFechaRP))
            {
                if (DateTime.TryParseExact(startFechaRP, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaExacta))
                {
                    baseQuery = baseQuery.Where(v => v.FECHA.Date == fechaExacta.Date);
                }
            }

            if (!string.IsNullOrEmpty(dni)) baseQuery = baseQuery.Where(v => v.DNI == dni);
            if (!string.IsNullOrEmpty(metodo)) baseQuery = baseQuery.Where(v => v.METODOOK == metodo);
            if (unidad != null && unidad.Any())
            {
                baseQuery = baseQuery.Where(v => unidad.Contains(v.UNIDAD_NOMBRE));
            }
            if (!string.IsNullOrEmpty(estudio)) baseQuery = baseQuery.Where(v => v.ESTUDIO == estudio);
            if (estadoPrograma.HasValue) baseQuery = baseQuery.Where(v => v.estado_Programa == estadoPrograma);
            if (estadoTurno.HasValue) baseQuery = baseQuery.Where(v => v.Estado_Turno_id == estadoTurno);
            if (!string.IsNullOrEmpty(usuario)) baseQuery = baseQuery.Where(e => e.usuario == usuario);

            if (!string.IsNullOrEmpty(prestador)) baseQuery = baseQuery.Where(v => v.PRESTADORQUEGENERASOLICITUD == prestador);
            if (!string.IsNullOrEmpty(obrasocial)) baseQuery = baseQuery.Where(v => v.OSCOD == obrasocial);
            if (!string.IsNullOrEmpty(ultimoContacto))
            {
                if (DateOnly.TryParse(ultimoContacto, out var fechaContacto))
                {
                    baseQuery = baseQuery.Where(e => e.fechaGestion == fechaContacto);
                }
            }
            if (inductor.HasValue) baseQuery = baseQuery.Where(v => v.INDUCTOR_ID == inductor);
            if (!string.IsNullOrEmpty(servicio))
            {
                baseQuery = baseQuery.Where(v => v.servicio == servicio);
            }
          
            var laboratorioItems = await baseQuery
                .Where(v => v.METODOOK == "Laboratorio")
                .GroupBy(v => v.idPedido)
                .Select(g => g.First())
                .ToListAsync();

            var otrosItems = await baseQuery
                .Where(v => v.METODOOK != "Laboratorio")
                .ToListAsync();

            var finalResult = laboratorioItems.Concat(otrosItems);
            return finalResult;
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
                DNI = item.DNI,
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
                motivo_no_turno=item.motivo_no_turno,
                seguimiento_estado_turno=item.seguimiento_estado_turno,
                seguimiento_cantidad_contactos=item.seguimiento_cantidad_contactos,
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
            var update = await _context.BEALTH_SOLPRACT_P_MANUAL.Where(e => e.IDPEDIDO == SolPract.IDPEDIDO).ToListAsync();
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
                query = query.Where(v => estadoTurno.Contains(v.Estado_Turno_id));
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
                FECHA = group.FirstOrDefault().FECHA,
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
                Estado_Turno_id = group.FirstOrDefault().Estado_Turno_id,
                SolPractBhDtos = group.Key == "Laboratorio" ? group.Take(1) :
                 group.Where(x => x.turno_id != null).GroupBy(x => new { x.idEstudio, x.turno_id }).Select(g => g.First())
                 .Concat(group.Where(x => x.turno_id == null))
            });

            return groupedResults;
        }
    }
}
