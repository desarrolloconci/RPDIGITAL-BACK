using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhServices;
using ValorModels.Models.BhModels;

namespace ValoresData.Services
{
    public class SolpractBhPedidoManualService : ISolPractBhPedidoManualService
    {
        private readonly ISolPractBhPedidoManualCmd _solPractBhPedidoManualCmd;
        public SolpractBhPedidoManualService(ISolPractBhPedidoManualCmd solPractBhPedidoManualCmd)
        {
            _solPractBhPedidoManualCmd = solPractBhPedidoManualCmd;
        }
        public async Task<bool> DeletRelSolPractBhPedidoManualoAsync(int id)
        {
            return await _solPractBhPedidoManualCmd.DeletRelSolPractBhPedidoManualoAsync(id);
        }

        public async Task<IEnumerable<SolPractBhPedidoManualModel>> GetSolPractBhPedidoManualAsync()
        {
            return await _solPractBhPedidoManualCmd.GetSolPractBhPedidoManualAsync();
        }

        public async Task<SolPractBhPedidoManualModel> GetSolPractBhPedidoManuallAsyncById(int id)
        {
            return await _solPractBhPedidoManualCmd.GetSolPractBhPedidoManuallAsyncById(id);
        }

        public async Task<bool> InsertSolPractBhPedidoManualAsync(SolPractBhPedidoManualModel pedidomanual)
        {
            pedidomanual.DNI = pedidomanual.DNI.TrimStart('0');
            return await _solPractBhPedidoManualCmd.InsertSolPractBhPedidoManualAsync(pedidomanual);
        }

        public async Task<bool> UpdateSolPractBhPedidoManualAsync(SolPractBhPedidoManualModel pedidomanual)
        {
            return await _solPractBhPedidoManualCmd.UpdateSolPractBhPedidoManualAsync(pedidomanual);
        }
    }
}
