using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CdmRp
{
    public class FichaPacienteCmd : IFichaPacienteCmd
    {
        private readonly DataBaseContext _context;
        public FichaPacienteCmd(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<FichaPacienteModel>> GetFichaPacienteAsync()
        {
            return await _context.FICHA_PACIENTES_BH.Take(1000).ToListAsync();
        }

        public async Task<FichaPacienteModel> GetFichaPacienteDetailAsync(int dni)
        {
            return await _context.FICHA_PACIENTES_BH.Where(e=> e.nro_documento == dni).FirstOrDefaultAsync();
        }
    }
}
