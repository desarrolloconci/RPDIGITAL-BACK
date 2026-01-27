using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models;

namespace ValoresData.Commands.CmdSolPract
{
    public class DatosPacientesCargaManualCmd : IDatosPacientesCargaManualCmd
    {
        private readonly DataBaseContext _context;
        public DatosPacientesCargaManualCmd( DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UltimoPedidoPorDniModel>> GetPacienteAsync(string dni)
        {
            return await _context.V_UltimoPedidoPorDni.Where(e => e.DNI == dni).ToListAsync();
        }
    }
}
