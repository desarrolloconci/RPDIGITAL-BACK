using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IMotivoBorradoPedidoCmd
    {
        public Task<IEnumerable<MotivoBorradoPedidoModel>> GetMotivoBorradoPedidoAsync();
    }
}
