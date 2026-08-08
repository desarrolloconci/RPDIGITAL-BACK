using ValorModels.Models.BhModels;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IMotivoBorradoPedidoService
    {
        public Task<IEnumerable<MotivoBorradoPedidoModel>> GetMotivoBorradoPedidoAsync();
    }
}
