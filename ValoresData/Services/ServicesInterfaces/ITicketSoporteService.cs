using ValorModels.Models.RpModels;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface ITicketSoporteService
    {
        public Task<TicketSoporteModel> InsertTicketSoporteAsync(TicketSoporteModel model);
        public Task<IEnumerable<TicketSoporteModel>> GetTicketsSoporteAsync(string? usuario = null, string? ticketId = null, string? startFecha = null, string? endFecha = null);
    }
}
