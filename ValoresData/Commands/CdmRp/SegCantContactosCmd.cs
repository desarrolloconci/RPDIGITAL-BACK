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
        private readonly ILogCambiosRpCmd _logCmd;
        public SegCantContactosCmd(DataBaseContext context, ILogCambiosRpCmd logCmd)
        {
            _context = context;
            _logCmd = logCmd;
        }
        public async Task<bool> DeleteSegCantContactoTotalAsync(SegCantContactosModel model, string? usuario = null)
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

            foreach (var entity in entities)
            {
                await _logCmd.RegistrarCambioAsync(model.idPedido, entity.idEstudio,
                    "Cantidad de contactos", "Cantidad de contactos", entity.cantidad.ToString(), null, usuario);
            }

            return true;
        }

        public async Task<bool> DeleteSegCantContactoUnitarioAsync(SegCantContactosModel model, string? usuario = null)
        {
            var entity = await _context.SEG_CANT_CONTACTOS
             .FirstOrDefaultAsync(e => e.idEstudio == model.idEstudio && e.idPedido == model.idPedido);

            if (entity is null)
            {
                return false;
            }

            _context.SEG_CANT_CONTACTOS.Remove(entity);
            await _context.SaveChangesAsync();

            await _logCmd.RegistrarCambioAsync(model.idPedido, model.idEstudio,
                "Cantidad de contactos", "Cantidad de contactos", entity.cantidad.ToString(), null, usuario);

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

            // Evita duplicar: solo inserta los estudios de este pedido que todavia no tienen fila.
            var estudiosExistentes = await _context.SEG_CANT_CONTACTOS
                .Where(e => e.idPedido == model.idPedido)
                .Select(e => e.idEstudio)
                .ToListAsync();

            var idEstudiosNuevos = estudios.Except(estudiosExistentes).ToList();

            var entidades = idEstudiosNuevos.Select(idestudio => new SegCantContactosModel
            {
                idPedido = model.idPedido,
                idEstudio = idestudio,
                cantidad = model.cantidad,
                fecha = model.fecha,
                id_usuario = model.id_usuario,
                MetodoOK=model.MetodoOK
            }).ToList();

            _context.SEG_CANT_CONTACTOS.AddRange(entidades);

            var resultVarios = await _context.SaveChangesAsync() > 0;

            if (resultVarios)
            {
                foreach (var idEstudio in idEstudiosNuevos)
                {
                    await _logCmd.RegistrarCambioAsync(model.idPedido, idEstudio,
                        "Cantidad de contactos", "Cantidad de contactos", null, model.cantidad.ToString(), model.id_usuario.ToString());
                }
            }

            return resultVarios;
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

            var result = await _context.SaveChangesAsync() > 0;

            if (result)
            {
                await _logCmd.RegistrarCambioAsync(model.idPedido, model.idEstudio,
                    "Cantidad de contactos", "Cantidad de contactos", null, model.cantidad.ToString(), model.id_usuario.ToString());
            }

            return result;
        }

        public async Task<bool> UpdateSegCantidadContactosAsync(SegCantContactosModel model)
        {
            var entity = await _context.SEG_CANT_CONTACTOS
                .FirstOrDefaultAsync(e => e.idPedido == model.idPedido && e.idEstudio == model.idEstudio);
            if (entity != null)
            {
                var cantidadAnterior = entity.cantidad;

                entity.idPedido = model.idPedido;
                entity.idEstudio = model.idEstudio;
                entity.MetodoOK = model.MetodoOK;
                entity.id_usuario = model.id_usuario;
                entity.cantidad = model.cantidad;
                entity.fecha = model.fecha;
                await _context.SaveChangesAsync();

                await _logCmd.RegistrarCambioAsync(model.idPedido, model.idEstudio,
                    "Cantidad de contactos", "Cantidad de contactos", cantidadAnterior.ToString(), model.cantidad.ToString(), model.id_usuario.ToString());

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

            var cantidadesAnteriores = entidades.ToDictionary(e => e.idEstudio, e => e.cantidad);

            foreach (var entity in entidades)
            {
                entity.MetodoOK = model.MetodoOK;
                entity.id_usuario = model.id_usuario;
                entity.cantidad = model.cantidad;
                entity.fecha = model.fecha;
            }

            var resultUpdateVarios = await _context.SaveChangesAsync() > 0;

            if (resultUpdateVarios)
            {
                foreach (var entity in entidades)
                {
                    await _logCmd.RegistrarCambioAsync(model.idPedido, entity.idEstudio,
                        "Cantidad de contactos", "Cantidad de contactos", cantidadesAnteriores[entity.idEstudio].ToString(), model.cantidad.ToString(), model.id_usuario.ToString());
                }
            }

            return resultUpdateVarios;
        }

        public async Task<IEnumerable<SegCantContactosModel>> GetSegCantidadContactos(SegCantContactosModel model)
        {
            var resul = await _context.SEG_CANT_CONTACTOS.Where(e => e.idEstudio == model.idEstudio && e.idPedido == model.idPedido).ToListAsync();
            return resul;
        }
    }
    
}
