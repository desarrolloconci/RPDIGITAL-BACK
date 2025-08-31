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
    public class GrupoEstudiosCmd : IGrupoEstudiosCmd
    {
        private readonly DataBaseContext _context;
        public GrupoEstudiosCmd(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<GrupoEstudiosModel>> GetGrupoEstudiosAsync()
        {
            return await _context.GRUPOESTUDIOS.ToListAsync();
        }
    }
}
