using Microsoft.EntityFrameworkCore;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdSolPract
{
    public class MotivoBorradoPedidoCmd : IMotivoBorradoPedidoCmd
    {
        private readonly DataBaseContext _context;
        public MotivoBorradoPedidoCmd(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MotivoBorradoPedidoModel>> GetMotivoBorradoPedidoAsync()
        {
            return await _context.MOTIVO_BORRADO_PEDIDO.Where(e => e.baja == false).ToListAsync();
        }
    }
}
