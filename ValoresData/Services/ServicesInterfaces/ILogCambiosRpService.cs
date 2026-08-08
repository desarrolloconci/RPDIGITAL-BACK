using System.Collections.Generic;
using System.Threading.Tasks;
using ValorModels.Models.RpModels;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface ILogCambiosRpService
    {
        public Task<IEnumerable<LogCambioRpModel>> GetLogCambiosAsync(string idPedido, string? idEstudio = null);
    }
}
