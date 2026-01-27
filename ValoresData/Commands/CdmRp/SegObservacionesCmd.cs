using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CdmRp
{
    public class SegObservacionesCmd : ISegObservacionesCmd
    {
        private readonly DataBaseContext _context;
        public SegObservacionesCmd(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SegObservacionesModel>> GetSegObservacionesAsync()
        {
            return await _context.SEG_OBSERVACIONES.ToListAsync();
        }

        public async Task<IEnumerable<SegObservacionesModel>> GetSegObservacionesById(SegObservacionesModel model)
        {
            return await _context.SEG_OBSERVACIONES.Where(e => e.idEstudio == model.idEstudio && e.idPedido == model.idPedido).ToListAsync();
        }

        public async Task<bool> InsertSegObservacionesAsync(SegObservacionesModel model)
        {
            if (model is null)
                return false;

            _context.SEG_OBSERVACIONES.Add(model);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> InsertSegObservacionesVarios(SegObservacionesModel model)
        {
            if (model == null)
                return false;

            var estudios = await _context.V_BEALTH_SOLPRAC
                .Where(x => x.IDPEDIDO == model.idPedido && x.METODOPRACTICA == model.Metodo)
                .Select(x => x.IDESTUDIO)
                .ToListAsync();

            if (!estudios.Any())
                return false;

            var entidades = estudios.Select(idestudio => new SegObservacionesModel
            {
                idPedido = model.idPedido,
                idEstudio = idestudio,
                idUsuario = model.idUsuario,
                observacion=model.observacion,
                Metodo = model.Metodo,
                Fecha = model.Fecha
            }).ToList();

            _context.SEG_OBSERVACIONES.AddRange(entidades);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSegObservacionesAsync(SegObservacionesModel model)
        {
            var entity = await _context.SEG_OBSERVACIONES
                .FirstOrDefaultAsync(e => e.idPedido == model.idPedido && e.idEstudio == model.idEstudio);
            if (entity != null)
            {
                entity.idPedido = model.idPedido;
                entity.idEstudio = model.idEstudio;
                entity.Metodo = model.Metodo;
                entity.observacion = model.observacion;
                entity.idUsuario = model.idUsuario;
                entity.Fecha = model.Fecha;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateSegObservacionesVarios(SegObservacionesModel model)
        {
            if (model == null)
                return false;

            var estudios = await _context.V_BEALTH_SOLPRAC
                .Where(x => x.IDPEDIDO == model.idPedido && x.METODOPRACTICA == model.Metodo)
                .Select(x => x.IDESTUDIO)
                .ToListAsync();

            if (!estudios.Any())
                return false;


            var entidades = await _context.SEG_OBSERVACIONES
                .Where(e => e.idPedido == model.idPedido && estudios.Contains(e.idEstudio))
                .ToListAsync();

            if (!entidades.Any())
                return false;

            foreach (var entity in entidades)
            {
                entity.Metodo = model.Metodo;
                entity.idUsuario = model.idUsuario;
                entity.observacion = model.observacion;
                entity.Fecha = model.Fecha;
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
