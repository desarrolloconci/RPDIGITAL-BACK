using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class SegUsuarioGestionCmd : ISegUsuarioGestionCmd
    {
        private readonly DataBaseContext _context;
        private readonly ILogCambiosRpCmd _logCmd;
        public SegUsuarioGestionCmd(DataBaseContext context, ILogCambiosRpCmd logCmd)
        {
            _context=context;
            _logCmd = logCmd;
        }

        private async Task<string?> GetNombreUsuarioAsync(int idUsuario)
        {
            var user = await _context.BH_USERS.FirstOrDefaultAsync(u => u.ID == idUsuario);
            if (user is null) return idUsuario.ToString();
            return $"{user.Last_name} {user.Name}".Trim();
        }

        public async Task<IEnumerable<SegUsuarioGestionModel>> GetSegUsuarioGestion(SegUsuarioGestionModel model)
        {
            var resul= await _context.SEG_USUARIO_GESTION.Where(e => e.idEstudio == model.idEstudio && e.idPedido == model.idPedido).ToListAsync();
            return resul;
        }
        public async Task<bool> InsertSegUsuarioGestionVarios(SegUsuarioGestionModel model)
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
            var estudiosExistentes = await _context.SEG_USUARIO_GESTION
                .Where(e => e.idPedido == model.idPedido)
                .Select(e => e.idEstudio)
                .ToListAsync();

            var idEstudiosNuevos = estudios.Except(estudiosExistentes).ToList();

            var entidades = idEstudiosNuevos.Select(idestudio => new SegUsuarioGestionModel
            {
                idPedido = model.idPedido,
                idEstudio = idestudio,
                Metodo = model.Metodo,
                fecha = model.fecha,
                idUsuario = model.idUsuario

            }).ToList();

            _context.SEG_USUARIO_GESTION.AddRange(entidades);

            var resultVarios = await _context.SaveChangesAsync() > 0;

            if (resultVarios)
            {
                var nombreUsuario = await GetNombreUsuarioAsync(model.idUsuario);
                foreach (var idEstudio in idEstudiosNuevos)
                {
                    await _logCmd.RegistrarCambioAsync(model.idPedido, idEstudio,
                        "Usuario de gestión", "Usuario de gestión", null, nombreUsuario, model.idUsuario.ToString());
                }
            }

            return resultVarios;
        }

        public async Task<bool> InsertSegUsuarioGestionAsync(SegUsuarioGestionModel model)
        {
            if (model is null)
                return false;

            _context.SEG_USUARIO_GESTION.Add(model);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                var nombreUsuario = await GetNombreUsuarioAsync(model.idUsuario);
                await _logCmd.RegistrarCambioAsync(model.idPedido, model.idEstudio,
                    "Usuario de gestión", "Usuario de gestión", null, nombreUsuario, model.idUsuario.ToString());
            }

            return result > 0;
        }

        public async Task<bool> UpdateSegUsuarioGestionAsync(SegUsuarioGestionModel model)
        {
            var entity = await _context.SEG_USUARIO_GESTION
                .FirstOrDefaultAsync(e => e.idPedido == model.idPedido && e.idEstudio == model.idEstudio);
            if (entity != null)
            {
                var idUsuarioAnterior = entity.idUsuario;

                entity.idPedido = model.idPedido;
                entity.idEstudio = model.idEstudio;
                entity.Metodo = model.Metodo;
                entity.idUsuario = model.idUsuario;
                entity.fecha = model.fecha;
                await _context.SaveChangesAsync();

                var nombreAnterior = await GetNombreUsuarioAsync(idUsuarioAnterior);
                var nombreNuevo = await GetNombreUsuarioAsync(model.idUsuario);
                await _logCmd.RegistrarCambioAsync(model.idPedido, model.idEstudio,
                    "Usuario de gestión", "Usuario de gestión", nombreAnterior, nombreNuevo, model.idUsuario.ToString());

                return true;
            }
            return false;
        }

        public async Task<bool> UpdateSegUsuarioGestionVarios(SegUsuarioGestionModel model)
        {
            if (model == null)
                return false;

            var estudios = await _context.V_BEALTH_SOLPRAC
                .Where(x => x.IDPEDIDO == model.idPedido && x.METODOOK == model.Metodo)
                .Select(x => x.IDESTUDIO)
                .ToListAsync();

            if (!estudios.Any())
                return false;


            var entidades = await _context.SEG_USUARIO_GESTION
                .Where(e => e.idPedido == model.idPedido && estudios.Contains(e.idEstudio))
                .ToListAsync();

            if (!entidades.Any())
                return false;

            var idsUsuarioAnterior = entidades.ToDictionary(e => e.idEstudio, e => e.idUsuario);

            foreach (var entity in entidades)
            {
                entity.Metodo = model.Metodo;
                entity.idUsuario = model.idUsuario;
                entity.fecha = model.fecha;
            }

            var resultUpdateVarios = await _context.SaveChangesAsync() > 0;

            if (resultUpdateVarios)
            {
                var nombreNuevo = await GetNombreUsuarioAsync(model.idUsuario);
                foreach (var entity in entidades)
                {
                    var nombreAnterior = await GetNombreUsuarioAsync(idsUsuarioAnterior[entity.idEstudio]);
                    await _logCmd.RegistrarCambioAsync(model.idPedido, entity.idEstudio,
                        "Usuario de gestión", "Usuario de gestión", nombreAnterior, nombreNuevo, model.idUsuario.ToString());
                }
            }

            return resultUpdateVarios;
        }
    }
}
