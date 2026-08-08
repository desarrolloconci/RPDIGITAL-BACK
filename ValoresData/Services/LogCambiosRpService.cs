using System.Collections.Generic;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models.RpModels;

namespace ValoresData.Services
{
    public class LogCambiosRpService : ILogCambiosRpService
    {
        private readonly ILogCambiosRpCmd _cmd;
        public LogCambiosRpService(ILogCambiosRpCmd cmd)
        {
            _cmd = cmd;
        }

        public async Task<IEnumerable<LogCambioRpModel>> GetLogCambiosAsync(string idPedido, string? idEstudio = null)
        {
            return await _cmd.GetLogCambiosAsync(idPedido, idEstudio);
        }
    }
}
