using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhInterfaces
{
    public interface IEstadoPedidoManualService
    {
        public Task<IEnumerable<EstadoPedidoManualModel>> GetEstadoPedidoManualAsync();

        public Task<bool> InsertEstadoPedidoManualAsync(EstadoPedidoManualModel estadoPedido);
        public Task<EstadoPedidoManualModel> GetEstadoPedidoManualById(int id);
        public Task<bool> UpdateEstadoPedidoManualAsync(EstadoPedidoManualModel estadoPedido);
        public Task<bool> DeleteEstadoPedidoManualAsyncVarios(EstadoPedidoManualModel estadoPedido);
    }
}
