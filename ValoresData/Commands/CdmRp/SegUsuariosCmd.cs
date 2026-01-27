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
    public class SegUsuariosCmd : ISegUsuariosCmd
    {
        private readonly DataBaseContext _context;
        public SegUsuariosCmd(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SegUsuariosModel>> GetSegUsuariosAsync()
        {
            return await _context.V_Seg_Usuarios.ToListAsync();
        }
    }
}
