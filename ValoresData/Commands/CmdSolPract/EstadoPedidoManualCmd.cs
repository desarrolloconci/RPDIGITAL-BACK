using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdSolPract
{
    public class EstadoPedidoManualCmd : IEstadoPedidoManualCmd
    {
        private readonly DataBaseContext _dbcontext;
        public EstadoPedidoManualCmd(DataBaseContext dataBaseContext)
        {
            _dbcontext = dataBaseContext;
        }

        public async Task<bool> DeleteEstadoPedidoManualAsyncVarios(EstadoPedidoManualModel estadoPedido)
        {
            var estado = await GetEstadoPedidoManualById(estadoPedido.id);
            if (estado is null)
            {
                return false;
            }
            _dbcontext.ESTADO_PEDIDO_MANUAL.Remove(estadoPedido);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EstadoPedidoManualModel>> GetEstadoPedidoManualAsync()
        {
            return await _dbcontext.ESTADO_PEDIDO_MANUAL.ToListAsync();
        }

        public async Task<EstadoPedidoManualModel> GetEstadoPedidoManualById(int id)
        {
            return await _dbcontext.ESTADO_PEDIDO_MANUAL.FindAsync(id);
        }

        public async Task<bool> InsertEstadoPedidoManualAsync(EstadoPedidoManualModel estadoPedido)
        {
            if (estadoPedido == null)
            {
                return false;
            }
            _dbcontext.ESTADO_PEDIDO_MANUAL.Add(estadoPedido);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEstadoPedidoManualAsync(EstadoPedidoManualModel estadoPedido)
        {
            var dbestado = await GetEstadoPedidoManualById(estadoPedido.id);
            if (dbestado != null)
            {
                dbestado.idpedido = estadoPedido.idpedido;
                dbestado.idtratamiento = estadoPedido.idtratamiento;
                dbestado.enproceso = estadoPedido.enproceso;
                dbestado.nocontactado = estadoPedido.nocontactado;
                dbestado.usuario = estadoPedido.usuario;
                dbestado.fecha = estadoPedido.fecha;

                await _dbcontext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<EstadoPedidoManualModel> GetRelSolPractAsync(string idPedido, string idEstudio)
        {
            return await _dbcontext.ESTADO_PEDIDO_MANUAL.FirstOrDefaultAsync(e => e.idpedido == idPedido && e.idtratamiento == idEstudio);
        }


    }
}
