using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos.BhDto;
using ValorModels.Models.BhModels;
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
     string? unidad = null,
     string? dni = null,
     string? metodo = null,
     string? prestador = null,
     string? estudio = null,
     int? estadoPrograma = null,
     string? estadoTurno = null,
     string? usuario = null,
     string? servicio = null,
     string? obrasocial = null,
     string? ultimoContacto = null,
     string? inductor = null)
        {
            // Crear la consulta base
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
                    estadoTurno = e.estadoPrograma,
                    fechaGestion = e.fechaGestion,
                    observaciones = e.observaciones,
                    creado = e.creado,
                    usuario = e.usuario,
                    ESTUDIO = v.ESTUDIO,
                    turno_id = e.turno_id,
                    METODOOK = v.METODOOK,
                    unidad = v.UNIDAD_ID,
                    CONFESPECIAL= v.CONFESPECIAL,
                    INDUCTOR= v.INDUCTOR,
                    Estado_pedido= v.Estado_pedido,
                    ATENDIDO = v.ATENDIDO,
                    UNIDAD_NOMBRE= v.UNIDAD,
                    estado_Programa= v.estado_Programa,
                    tur_fecha=e.tur_fecha,
                };

            // Aplicar filtros 
            if (!string.IsNullOrEmpty(startFechaRP) && !string.IsNullOrEmpty(endFechaRP))
            {
                query = query.Where(v => v.FECHA >= DateTime.Parse(startFechaRP) && v.FECHA <= DateTime.Parse(endFechaRP));
            }
            if (!string.IsNullOrEmpty(dni))
            {
                query = query.Where(v => v.DNI == dni);
            }

            if (!string.IsNullOrEmpty(metodo))
            {
                query = query.Where(v => v.METODOOK == metodo);
            }

            if (!string.IsNullOrEmpty(unidad))
            {
                query = query.Where(v => v.unidad ==int.Parse( unidad));
            }

            if (!string.IsNullOrEmpty(estudio))
            {
                query = query.Where(v => v.ESTUDIO == estudio);
            }

            if (estadoPrograma.HasValue)
            {
                query = query.Where(v => v.estado_Programa == estadoPrograma);
            }
            if (!string.IsNullOrEmpty(estadoTurno))
            {
                query = query.Where(e => e.estadoTurno == estadoTurno);
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
                query = query.Where(v => v.OBRASOCIAL == obrasocial);
            }
            if (!string.IsNullOrEmpty(ultimoContacto))
            {
                query = query.Where(e => e.fechaGestion == DateOnly.Parse(ultimoContacto));
            }
            if (!string.IsNullOrEmpty(inductor))
            {
                query = query.Where(v => v.INDUCTOR == inductor);
            }


            var resultados = await query.ToListAsync();


            var resultadosFiltrados = resultados
                .Where(v => !string.IsNullOrWhiteSpace(v.DNI) && v.unidad != null) 
                .GroupBy(v => new { v.DNI, v.unidad }) 
                .SelectMany(g => g.Take(1)) 
                .Take(100);

            return resultadosFiltrados;
        }

        public async Task<IEnumerable<SolPractBhDto>> GetSolPractAsync(
     DateTime? fechaCreacionRP = null,
     string? startFechaRP = null,
     string? endFechaRP = null,
     string? unidad = null,
     string? dni = null,
     string? metodo = null,
     string? prestador = null,
     string? estudio = null,
     int? estadoPrograma = null,
     string? estadoTurno = null,
     string? usuario= null,
     string? servicio = null,
     string? obrasocial= null,
     string? ultimoContacto = null,
     string? inductor = null
     )


        {
            // Crear la consulta base
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
                    estadoTurno= e.estadoPrograma,
                    fechaGestion = e.fechaGestion,
                    observaciones = e.observaciones,
                    creado = e.creado,
                    usuario = e.usuario,
                    ESTUDIO = v.ESTUDIO,
                    turno_id = e.turno_id,
                    METODOOK = v.METODOOK,
                    unidad = v.UNIDAD_ID,
                    servicio = v.IDSERVICIOSOLICITUD,
                    CONFESPECIAL = v.CONFESPECIAL,
                    INDUCTOR = v.INDUCTOR,
                    Estado_pedido = v.Estado_pedido,
                    ATENDIDO = v.ATENDIDO,
                    UNIDAD_NOMBRE = v.UNIDAD,
                    estado_Programa = v.estado_Programa,
                     tur_fecha = e.tur_fecha,
                };

            // Aplicar filtros 
            if (!string.IsNullOrEmpty(startFechaRP) && !string.IsNullOrEmpty(endFechaRP))
            {
                query = query.Where(v => v.FECHA >= DateTime.Parse(startFechaRP) && v.FECHA <= DateTime.Parse(endFechaRP));
            }
            if (!string.IsNullOrEmpty(dni))
            {
                query = query.Where(v => v.DNI == dni);
            }

            if (!string.IsNullOrEmpty(metodo))
            {
                query = query.Where(v => v.METODOOK == metodo);
            }

            if (!string.IsNullOrEmpty(unidad))
            {
                query = query.Where(v => v.unidad == int.Parse(unidad));
            }

            if (!string.IsNullOrEmpty(estudio))
            {
                query = query.Where(v => v.ESTUDIO == estudio);
            }

            if (estadoPrograma.HasValue)
            {
                query = query.Where(v => v.estado_Programa == estadoPrograma);
            }
            if (!string.IsNullOrEmpty(estadoTurno))
            {
                query = query.Where(e => e.estadoTurno == estadoTurno);
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
                query = query.Where(v => v.OBRASOCIAL == obrasocial);
            }
            if (!string.IsNullOrEmpty(ultimoContacto))
            {
                query = query.Where(e => e.fechaGestion == DateOnly.Parse(ultimoContacto));
            }
            if (!string.IsNullOrEmpty(inductor))
            {
                query = query.Where(V => V.INDUCTOR == inductor);
            }
            var allResults = await query.ToListAsync();

            // Aplicar lógica para "Laboratorio"
            var groupedResults = allResults
                .GroupBy(r => r.METODOOK)
                .SelectMany(group =>
                {
                    if (group.Key == "Laboratorio")
                    {
                        // Traer solo un registro si el método es "Laboratorio"
                        return group.Take(1);
                    }
                    else
                    {
                        // Traer todos los registros para otros métodos
                        return group.Take(100);
                    }
                });

            return groupedResults;
        }
    }
}
