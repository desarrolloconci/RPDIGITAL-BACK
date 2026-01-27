using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos.RpDto;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CdmRp
{
   public class SegCantContactosCmd : ISegCantContactosCmd
    {
        private readonly DataBaseContext _context;
        public SegCantContactosCmd(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteSegCantContactoTotalAsync(SegCantContactosModel model)
        {
            var entities = await _context.SEG_CANT_CONTACTOS
                .Where(e => e.idPedido == model.idPedido && e.MetodoOK == model.MetodoOK)
                .ToListAsync();

            if (entities == null || !entities.Any())
            {
                return false;
            }
            _context.SEG_CANT_CONTACTOS.RemoveRange(entities);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteSegCantContactoUnitarioAsync(SegCantContactosModel model)
        {
            var entity = await _context.SEG_CANT_CONTACTOS
             .FirstOrDefaultAsync(e => e.idEstudio == model.idEstudio && e.idPedido == model.idPedido);

            if (entity is null)
            {
                return false;
            }

            _context.SEG_CANT_CONTACTOS.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertSegCantContactotVarios(SegCantContactosModel model)
        {
            if (model == null)
                return false;

            var estudios = await _context.V_BEALTH_SOLPRAC
                .Where(x => x.IDPEDIDO == model.idPedido && x.METODOOK == model.MetodoOK)
                .Select(x => x.IDESTUDIO)
                .ToListAsync();

            if (!estudios.Any())
                return false;

            var entidades = estudios.Select(idestudio => new SegCantContactosModel
            {
                idPedido = model.idPedido,
                idEstudio = idestudio,
                cantidad = model.cantidad,
                fecha = model.fecha,
                id_usuario = model.id_usuario,
                MetodoOK=model.MetodoOK
            }).ToList();

            _context.SEG_CANT_CONTACTOS.AddRange(entidades);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> InsertSegCantContactosAsync(SegCantContactosModel model)
        {
            if (model == null)
                return false;

            var entity = new SegCantContactosModel
            {
                idPedido = model.idPedido,
                idEstudio = model.idEstudio,
                cantidad = model.cantidad,
                fecha = model.fecha,
                id_usuario = model.id_usuario,
                MetodoOK = model.MetodoOK
            };

            _context.SEG_CANT_CONTACTOS.Add(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSegCantidadContactosAsync(SegCantContactosModel model)
        {
            var entity = await _context.SEG_CANT_CONTACTOS
                .FirstOrDefaultAsync(e => e.idPedido == model.idPedido && e.idEstudio == model.idEstudio);
            if (entity != null)
            {
                entity.idPedido = model.idPedido;
                entity.idEstudio = model.idEstudio;
                entity.MetodoOK = model.MetodoOK;
                entity.id_usuario = model.id_usuario;
                entity.cantidad = model.cantidad;
                entity.fecha = model.fecha;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateSegCantidadContactosVarios(SegCantContactosModel model)
        {
            if (model == null)
                return false;

            var estudios = await _context.V_BEALTH_SOLPRAC
                .Where(x => x.IDPEDIDO == model.idPedido && x.METODOOK == model.MetodoOK)
                .Select(x => x.IDESTUDIO)
                .ToListAsync();

            if (!estudios.Any())
                return false;


            var entidades = await _context.SEG_CANT_CONTACTOS
                .Where(e => e.idPedido == model.idPedido && estudios.Contains(e.idEstudio))
                .ToListAsync();

            if (!entidades.Any())
                return false;

            foreach (var entity in entidades)
            {
                entity.MetodoOK = model.MetodoOK;
                entity.id_usuario = model.id_usuario;
                entity.cantidad = model.cantidad;
                entity.fecha = model.fecha;
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<SegCantContactosModel>> GetSegCantidadContactos(SegCantContactosModel model)
        {
            var resul = await _context.SEG_CANT_CONTACTOS.Where(e => e.idEstudio == model.idEstudio && e.idPedido == model.idPedido).ToListAsync();
            return resul;
        }
    }
    
}
