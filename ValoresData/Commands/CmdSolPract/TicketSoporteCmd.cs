using Microsoft.EntityFrameworkCore;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CmdSolPract
{
    public class TicketSoporteCmd : ITicketSoporteCmd
    {
        private readonly DataBaseContext _context;
        public TicketSoporteCmd(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<TicketSoporteModel> InsertTicketSoporteAsync(TicketSoporteModel model)
        {
            // Todo ticket nuevo entra como "Pendiente": no se confia en lo que mande el cliente.
            model.estado = "Pendiente";
            model.respuesta = null;
            model.fechaRespuesta = null;

            _context.TICKETS_SOPORTE.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<TicketSoporteModel>> GetTicketsSoporteAsync(string? usuario = null, string? ticketId = null, string? startFecha = null, string? endFecha = null)
        {
            var query = _context.TICKETS_SOPORTE.AsQueryable();

            if (!string.IsNullOrEmpty(usuario))
                query = query.Where(e => e.usuario != null && e.usuario.Contains(usuario));

            if (!string.IsNullOrEmpty(ticketId))
                query = query.Where(e => e.ticketId != null && e.ticketId.Contains(ticketId));

            if (!string.IsNullOrEmpty(startFecha) && DateTime.TryParse(startFecha, out var start))
                query = query.Where(e => e.fecha >= start.Date);

            if (!string.IsNullOrEmpty(endFecha) && DateTime.TryParse(endFecha, out var end))
                query = query.Where(e => e.fecha <= end.Date.AddDays(1).AddTicks(-1));

            return await query.OrderByDescending(e => e.fecha).ToListAsync();
        }
    }
}
