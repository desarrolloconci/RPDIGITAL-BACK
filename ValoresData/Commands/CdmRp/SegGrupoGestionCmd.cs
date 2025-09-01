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
   public class SegGrupoGestionCmd : ISegGrupoGestionCmd
    {
        private readonly DataBaseContext _context;
        public SegGrupoGestionCmd(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SegGrupoGestionModel>> GetSegGrupoGestionAsync()
        {
            return await _context.SEG_GRUPO_GESTION.ToListAsync();
        }
    }
}
