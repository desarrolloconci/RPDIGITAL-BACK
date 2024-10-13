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
       string unidad = null,
       string dni = null,
       string metodo = null,
       string prestador = null,
       string estudio = null,
       string estadoPractica = null)
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
                    estado = e.estado,
                    fechaGestion = e.fechaGestion,
                    observaciones = e.observaciones,
                    creado = e.creado,
                    usuario = e.usuario,
                    ESTUDIO = v.ESTUDIO,
                    turno_id = e.turno_id,
                    METODOPRACTICA = v.METODOPRACTICA,
                    unidad = v.UNIDAD,
                };

            // Aplicar filtros 
            if (!string.IsNullOrEmpty(dni))
            {
                query = query.Where(v => v.DNI == dni);
            }

            if (!string.IsNullOrEmpty(metodo))
            {
                query = query.Where(v => v.METODOPRACTICA == metodo);
            }

            if (!string.IsNullOrEmpty(prestador))
            {
                query = query.Where(v => v.PRESTADORQUEGENERASOLICITUD == prestador);
            }

            if (!string.IsNullOrEmpty(estudio))
            {
                query = query.Where(v => v.ESTUDIO == estudio);
            }

            if (!string.IsNullOrEmpty(estadoPractica))
            {
                query = query.Where(e => e.estado == estadoPractica);
            }


            var resultados = await query.ToListAsync();


            var resultadosFiltrados = resultados
                .GroupBy(v => new { v.DNI })
                .SelectMany(g =>
                    g.Key.DNI.Trim() != null
                    ? g.Take(1)
                    : g)
                .Take(100);

            return resultadosFiltrados;
        }

        public async Task<IEnumerable<SolPractBhDto>> GetSolPractAsync(
     DateTime? fechaCreacionRP = null,
     string unidad = null,
     string dni = null,
     string metodo = null,
     string prestador = null,
     string estudio = null,
     string estadoPractica = null)
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
                    estado = e.estado,
                    fechaGestion = e.fechaGestion,
                    observaciones = e.observaciones,
                    creado = e.creado,
                    usuario = e.usuario,
                    ESTUDIO = v.ESTUDIO,
                    turno_id = e.turno_id,
                    METODOPRACTICA = v.METODOPRACTICA,
                    unidad = v.UNIDAD,
                };

            // Aplicar filtros 
            if (!string.IsNullOrEmpty(dni))
            {
                query = query.Where(v => v.DNI == dni);
            }

            if (!string.IsNullOrEmpty(metodo))
            {
                query = query.Where(v => v.METODOPRACTICA == metodo);
            }

            if (!string.IsNullOrEmpty(prestador))
            {
                query = query.Where(v => v.PRESTADORQUEGENERASOLICITUD == prestador);
            }

            if (!string.IsNullOrEmpty(estudio))
            {
                query = query.Where(v => v.ESTUDIO == estudio);
            }

            if (!string.IsNullOrEmpty(estadoPractica))
            {
                query = query.Where(e => e.estado == estadoPractica);
            }


            var resultados = await query.Take(100).ToListAsync();


       

            return resultados;
        }
    }
}
