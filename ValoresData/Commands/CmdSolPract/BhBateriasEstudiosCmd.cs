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
    public class BhBateriasEstudiosCmd : IBhBateriasEstudiosCmd
    {
        private readonly DataBaseContext _context;
        public BhBateriasEstudiosCmd(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BhBateriasModel>> GeBhBateriasEstudiosAsync()
        {
            return await _context.v_BH_BATERIAS.ToListAsync();
        }

        public async Task<IEnumerable<BhBateriasEstudiosModel>> GetBhBateriasEstudiosGrupoAsync(int GrupoId)
        {
           return await _context.V_BH_BATERIAS_ESTUDIOS.Where(e => e.GRUPO_ID == GrupoId && e.baja==false).ToListAsync();
        }
    }
}
