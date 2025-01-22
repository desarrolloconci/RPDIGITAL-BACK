using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IEstadoPedidoManualCmd
    {
        public Task<IEnumerable<EstadoPedidoManualModel>> GetEstadoPedidoManualAsync();

        public Task<bool> InsertEstadoPedidoManualAsync(EstadoPedidoManualModel estadoPedido);
        public Task<EstadoPedidoManualModel> GetEstadoPedidoManualById(int id);
        public Task<bool> UpdateEstadoPedidoManualAsync(EstadoPedidoManualModel estadoPedido);
        public Task<bool> DeleteEstadoPedidoManualAsyncVarios(EstadoPedidoManualModel estadoPedido);
        public Task<EstadoPedidoManualModel> GetRelSolPractAsync(string idPedido, string idEstudio);
    }
}
