using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CmdSolPract
{
    public class LogCambiosRpCmd : ILogCambiosRpCmd
    {
        private readonly DataBaseContext _context;
        public LogCambiosRpCmd(DataBaseContext context)
        {
            _context = context;
        }

        public async Task RegistrarCambioAsync(
            string idPedido,
            string? idEstudio,
            string accion,
            string? campo,
            string? valorAnterior,
            string? valorNuevo,
            string? usuario,
            int? idTurno = null)
        {
            // No tiene sentido dejar registro si en los hechos no cambio nada.
            if (valorAnterior == valorNuevo) return;

            _context.SEG_LOG_CAMBIOS_RP.Add(new LogCambioRpModel
            {
                idPedido = idPedido,
                idEstudio = idEstudio,
                accion = accion,
                campo = campo,
                valorAnterior = valorAnterior,
                valorNuevo = valorNuevo,
                idTurno = idTurno,
                usuario = await ResolverNombreUsuarioAsync(usuario),
                fecha = DateTime.Now,
            });

            await _context.SaveChangesAsync();
        }

        // Algunas pantallas mandan el nombre de usuario directo, otras solo el ID numerico
        // (BH_USERS.ID). Para que el log siempre muestre algo legible, si "usuario" es un
        // numero se resuelve contra BH_USERS; si no, se deja tal cual vino.
        private async Task<string?> ResolverNombreUsuarioAsync(string? usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario) || !int.TryParse(usuario, out var idUsuario))
                return usuario;

            var user = await _context.BH_USERS.FirstOrDefaultAsync(u => u.ID == idUsuario);
            if (user is null) return usuario;

            return $"{user.Last_name} {user.Name}".Trim();
        }

        public async Task<IEnumerable<LogCambioRpModel>> GetLogCambiosAsync(string idPedido, string? idEstudio = null)
        {
            var query = _context.SEG_LOG_CAMBIOS_RP.Where(e => e.idPedido == idPedido);

            // Los cambios de datos del paciente (obra social, email, etc.) se guardan con idEstudio null
            // porque son a nivel pedido, no de un estudio en particular: siguen mostrandose en cualquier estudio.
            if (!string.IsNullOrEmpty(idEstudio))
                query = query.Where(e => e.idEstudio == idEstudio || e.idEstudio == null);

            return await query.OrderByDescending(e => e.fecha).ToListAsync();
        }
    }
}
