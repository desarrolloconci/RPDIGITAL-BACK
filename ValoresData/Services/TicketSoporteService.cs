using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models.RpModels;

namespace ValoresData.Services
{
    public class TicketSoporteService : ITicketSoporteService
    {
        private readonly ITicketSoporteCmd _cmd;
        public TicketSoporteService(ITicketSoporteCmd cmd)
        {
            _cmd = cmd;
        }

        public async Task<TicketSoporteModel> InsertTicketSoporteAsync(TicketSoporteModel model)
        {
            return await _cmd.InsertTicketSoporteAsync(model);
        }

        public async Task<IEnumerable<TicketSoporteModel>> GetTicketsSoporteAsync(string? usuario = null, string? ticketId = null, string? startFecha = null, string? endFecha = null)
        {
            return await _cmd.GetTicketsSoporteAsync(usuario, ticketId, startFecha, endFecha);
        }
    }
}
