using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CdmRp
{
    public class RelSegMotivoNoTurnoCmd : IRelSegMotivoNoTurnoCmd
    {
        private readonly DataBaseContext _context;
        private readonly ILogCambiosRpCmd _logCmd;
        public RelSegMotivoNoTurnoCmd(DataBaseContext context, ILogCambiosRpCmd logCmd)
        {
            _context = context;
            _logCmd = logCmd;
        }

        private async Task<string?> GetNombreMotivoAsync(int? idMotivo)
        {
            if (idMotivo is null) return null;
            return (await _context.SEG_MOTIVO_NO_TURNO.FirstOrDefaultAsync(m => m.id == idMotivo))?.motivo_no_turno;
        }

        public async Task<IEnumerable<RelSegMotivoNoTurnoModel>> GetRelSegMotivoNoTurnoAsync()
        {
            return await _context.SEG_REL_MOTIVO_NO_TURNO.ToListAsync();
        }

        public async Task<IEnumerable<RelSegMotivoNoTurnoModel>> GetRelSegMotivoById(RelSegMotivoNoTurnoModel model)
        {
            var resul = await _context.SEG_REL_MOTIVO_NO_TURNO.Where(e => e.idEstudio == model.idEstudio && e.idPedido == model.idPedido).ToListAsync();
            return resul;
        }

        public async Task<bool> InsertRelSegMotivoNoTurnoAsync(RelSegMotivoNoTurnoModel model)
        {

            if (model is null)
                return false;

            _context.SEG_REL_MOTIVO_NO_TURNO.Add(model);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                var nombreMotivo = await GetNombreMotivoAsync(model.id_motivo_no_turno);
                await _logCmd.RegistrarCambioAsync(model.idPedido, model.idEstudio,
                    "Motivo no turno", "Motivo", null, nombreMotivo, model.id_usuario.ToString());
            }

            return result > 0;
        }
        public async Task<bool> InsertRelSegMotivoNoVarios(RelSegMotivoNoTurnoModel model)
        {
            if (model == null)
                return false;

            var estudios = await _context.V_BEALTH_SOLPRAC
                .Where(x => x.IDPEDIDO == model.idPedido && x.METODOOK == model.Metodo)
                .Select(x => x.IDESTUDIO)
                .ToListAsync();

            if (!estudios.Any())
                return false;

            // Evita duplicar: solo inserta los estudios de este pedido que todavia no tienen fila.
            var estudiosExistentes = await _context.SEG_REL_MOTIVO_NO_TURNO
                .Where(e => e.idPedido == model.idPedido)
                .Select(e => e.idEstudio)
                .ToListAsync();

            var entidades = estudios.Except(estudiosExistentes).Select(idestudio => new RelSegMotivoNoTurnoModel
            {
               idPedido=model.idPedido,
               idEstudio = idestudio,
               id_motivo_no_turno =model.id_motivo_no_turno,
               id_usuario=model.id_usuario,
                Metodo = model.Metodo,
               fecha= model.fecha
            }).ToList();

            _context.SEG_REL_MOTIVO_NO_TURNO.AddRange(entidades);

            var resultVarios = await _context.SaveChangesAsync() > 0;

            if (resultVarios)
            {
                var nombreMotivo = await GetNombreMotivoAsync(model.id_motivo_no_turno);
                foreach (var entidad in entidades)
                {
                    await _logCmd.RegistrarCambioAsync(model.idPedido, entidad.idEstudio,
                        "Motivo no turno", "Motivo", null, nombreMotivo, model.id_usuario.ToString());
                }
            }

            return resultVarios;
        }
        public async Task<bool> UpdateRelSegMotivoNoAsync(RelSegMotivoNoTurnoModel model)
        {
            var entity = await _context.SEG_REL_MOTIVO_NO_TURNO
                .FirstOrDefaultAsync(e => e.idPedido == model.idPedido && e.idEstudio == model.idEstudio);
            if (entity != null)
            {
                var idMotivoAnterior = entity.id_motivo_no_turno;

                entity.idPedido = model.idPedido;
                entity.idEstudio = model.idEstudio;
                entity.Metodo = model.Metodo;
                entity.id_motivo_no_turno = model.id_motivo_no_turno;
                entity.id_usuario = model.id_usuario;
                entity.fecha = model.fecha;
                await _context.SaveChangesAsync();

                var nombreAnterior = await GetNombreMotivoAsync(idMotivoAnterior);
                var nombreNuevo = await GetNombreMotivoAsync(model.id_motivo_no_turno);
                await _logCmd.RegistrarCambioAsync(model.idPedido, model.idEstudio,
                    "Motivo no turno", "Motivo", nombreAnterior, nombreNuevo, model.id_usuario.ToString());

                return true;
            }
            return false;
        }

        public async Task<bool> UpdateRelSegMotivoNoVarios(RelSegMotivoNoTurnoModel model)
        {
            if (model == null)
                return false;

            var estudios = await _context.V_BEALTH_SOLPRAC
                .Where(x => x.IDPEDIDO == model.idPedido && x.METODOOK == model.Metodo)
                .Select(x => x.IDESTUDIO)
                .ToListAsync();

            if (!estudios.Any())
                return false;


            var entidades = await _context.SEG_REL_MOTIVO_NO_TURNO
                .Where(e => e.idPedido == model.idPedido && estudios.Contains(e.idEstudio))
                .ToListAsync();

            if (!entidades.Any())
                return false;

            var idsMotivoAnterior = entidades.ToDictionary(e => e.idEstudio, e => e.id_motivo_no_turno);

            foreach (var entity in entidades)
            {
                entity.Metodo = model.Metodo;
                entity.id_usuario = model.id_usuario;
                entity.id_motivo_no_turno = model.id_motivo_no_turno;
                entity.fecha = model.fecha;
            }

            var resultUpdateVarios = await _context.SaveChangesAsync() > 0;

            if (resultUpdateVarios)
            {
                var nombreNuevo = await GetNombreMotivoAsync(model.id_motivo_no_turno);
                foreach (var entity in entidades)
                {
                    var nombreAnterior = await GetNombreMotivoAsync(idsMotivoAnterior[entity.idEstudio]);
                    await _logCmd.RegistrarCambioAsync(model.idPedido, entity.idEstudio,
                        "Motivo no turno", "Motivo", nombreAnterior, nombreNuevo, model.id_usuario.ToString());
                }
            }

            return resultUpdateVarios;
        }

        public async Task<bool> DeleteRelSegMotivoNoTurnoAsync(string idPedido, string idEstudio, string? usuario = null)
        {
            var entity = await _context.SEG_REL_MOTIVO_NO_TURNO
                .FirstOrDefaultAsync(e => e.idPedido == idPedido && e.idEstudio == idEstudio);

            if (entity is null) return false;

            var nombreMotivo = await GetNombreMotivoAsync(entity.id_motivo_no_turno);

            _context.SEG_REL_MOTIVO_NO_TURNO.Remove(entity);
            await _context.SaveChangesAsync();

            await _logCmd.RegistrarCambioAsync(idPedido, idEstudio,
                "Motivo no turno", "Motivo", nombreMotivo, null, usuario);

            return true;
        }
    }
}
