using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhServices
{
    public interface ISolPractBhPedidoManualService
    {
        public Task<IEnumerable<SolPractBhPedidoManualModel>> GetSolPractBhPedidoManualAsync();
        public Task<bool> InsertSolPractBhPedidoManualAsync(SolPractBhPedidoManualModel pedidomanual);
        public Task<SolPractBhPedidoManualModel> GetSolPractBhPedidoManuallAsyncById(int id);
        public Task<bool> UpdateSolPractBhPedidoManualAsync(SolPractBhPedidoManualModel pedidomanual);
        public Task<bool> DeletRelSolPractBhPedidoManualoAsync(int id);
    }
}
