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
        private readonly ILogCambiosRpCmd _logCmd;
        public SegObservacionesCmd(DataBaseContext context, ILogCambiosRpCmd logCmd)
        {
            _context = context;
            _logCmd = logCmd;
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

            if (result > 0)
            {
                await _logCmd.RegistrarCambioAsync(model.idPedido, model.idEstudio,
                    "Observacion", "Observacion", null, model.observacion, model.idUsuario.ToString());
            }

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

            // Evita duplicar: solo inserta los estudios de este pedido que todavia no tienen fila.
            var estudiosExistentes = await _context.SEG_OBSERVACIONES
                .Where(e => e.idPedido == model.idPedido)
                .Select(e => e.idEstudio)
                .ToListAsync();

            var entidades = estudios.Except(estudiosExistentes).Select(idestudio => new SegObservacionesModel
            {
                idPedido = model.idPedido,
                idEstudio = idestudio,
                idUsuario = model.idUsuario,
                observacion=model.observacion,
                Metodo = model.Metodo,
                Fecha = model.Fecha
            }).ToList();

            _context.SEG_OBSERVACIONES.AddRange(entidades);

            var resultVarios = await _context.SaveChangesAsync() > 0;

            if (resultVarios)
            {
                foreach (var entidad in entidades)
                {
                    await _logCmd.RegistrarCambioAsync(model.idPedido, entidad.idEstudio,
                        "Observacion", "Observacion", null, model.observacion, model.idUsuario.ToString());
                }
            }

            return resultVarios;
        }

        public async Task<bool> UpdateSegObservacionesAsync(SegObservacionesModel model)
        {
            var entity = await _context.SEG_OBSERVACIONES
                .FirstOrDefaultAsync(e => e.idPedido == model.idPedido && e.idEstudio == model.idEstudio);
            if (entity != null)
            {
                var observacionAnterior = entity.observacion;

                entity.idPedido = model.idPedido;
                entity.idEstudio = model.idEstudio;
                entity.Metodo = model.Metodo;
                entity.observacion = model.observacion;
                entity.idUsuario = model.idUsuario;
                entity.Fecha = model.Fecha;
                await _context.SaveChangesAsync();

                await _logCmd.RegistrarCambioAsync(model.idPedido, model.idEstudio,
                    "Observacion", "Observacion", observacionAnterior, model.observacion, model.idUsuario.ToString());

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

            var observacionesAnteriores = entidades.ToDictionary(e => e.idEstudio, e => e.observacion);

            foreach (var entity in entidades)
            {
                entity.Metodo = model.Metodo;
                entity.idUsuario = model.idUsuario;
                entity.observacion = model.observacion;
                entity.Fecha = model.Fecha;
            }

            var resultUpdateVarios = await _context.SaveChangesAsync() > 0;

            if (resultUpdateVarios)
            {
                foreach (var entity in entidades)
                {
                    await _logCmd.RegistrarCambioAsync(model.idPedido, entity.idEstudio,
                        "Observacion", "Observacion", observacionesAnteriores[entity.idEstudio], model.observacion, model.idUsuario.ToString());
                }
            }

            return resultUpdateVarios;
        }

        public async Task<bool> DeleteSegObservacionesAsync(string idPedido, string idEstudio, string? usuario = null)
        {
            var entity = await _context.SEG_OBSERVACIONES
                .FirstOrDefaultAsync(e => e.idPedido == idPedido && e.idEstudio == idEstudio);

            if (entity is null) return false;

            var observacionAnterior = entity.observacion;

            _context.SEG_OBSERVACIONES.Remove(entity);
            await _context.SaveChangesAsync();

            await _logCmd.RegistrarCambioAsync(idPedido, idEstudio,
                "Observacion", "Observacion", observacionAnterior, null, usuario);

            return true;
        }
    }
}
