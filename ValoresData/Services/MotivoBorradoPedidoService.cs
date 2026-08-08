using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models.BhModels;

namespace ValoresData.Services
{
    public class MotivoBorradoPedidoService : IMotivoBorradoPedidoService
    {
        private readonly IMotivoBorradoPedidoCmd _cmd;
        public MotivoBorradoPedidoService(IMotivoBorradoPedidoCmd cmd)
        {
            _cmd = cmd;
        }

        public async Task<IEnumerable<MotivoBorradoPedidoModel>> GetMotivoBorradoPedidoAsync()
        {
            return await _cmd.GetMotivoBorradoPedidoAsync();
        }
    }
}
