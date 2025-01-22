using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhServices
{
    public class EstadoPedidoManualService : IEstadoPedidoManualService
    {
        private readonly IEstadoPedidoManualCmd _estadoPedidoManualCmd;
        public EstadoPedidoManualService(IEstadoPedidoManualCmd estadoPedidoManualCmd)
        {
            _estadoPedidoManualCmd = estadoPedidoManualCmd;
        }

        public async Task<bool> DeleteEstadoPedidoManualAsyncVarios(EstadoPedidoManualModel estadoPedido)
        {
            return await _estadoPedidoManualCmd.DeleteEstadoPedidoManualAsyncVarios(estadoPedido);
        }

        public async Task<IEnumerable<EstadoPedidoManualModel>> GetEstadoPedidoManualAsync()
        {
            return await _estadoPedidoManualCmd.GetEstadoPedidoManualAsync();
        }

        public async Task<EstadoPedidoManualModel> GetEstadoPedidoManualById(int id)
        {
            return await _estadoPedidoManualCmd.GetEstadoPedidoManualById(id);
        }

        public async Task<bool> InsertEstadoPedidoManualAsync(EstadoPedidoManualModel estadoPedido)
        {
            var turno1 = await _estadoPedidoManualCmd.GetRelSolPractAsync(estadoPedido.idpedido, estadoPedido.idtratamiento);
        /*    var turno = await _estadoPedidoManualCmd.GetEstadoPedidoManualById(estadoPedido.id);
            if (turno != null)
            {
                turno.idpedido = estadoPedido.idpedido;
                turno.idtratamiento = estadoPedido.idtratamiento;
                turno.enproceso = estadoPedido.enproceso;
                turno.nocontactado = estadoPedido.nocontactado;
                turno.usuario = estadoPedido.usuario;
                turno.fecha = estadoPedido.fecha;
            } */

                if (turno1 == null)
            {
                await _estadoPedidoManualCmd.InsertEstadoPedidoManualAsync(estadoPedido);
                return true;
            }
            await _estadoPedidoManualCmd.DeleteEstadoPedidoManualAsyncVarios(turno1);
            return true;
        }


        public async Task<bool> UpdateEstadoPedidoManualAsync(EstadoPedidoManualModel estadoPedido)
        {
            return await _estadoPedidoManualCmd.UpdateEstadoPedidoManualAsync(estadoPedido);
        }
    }
}
