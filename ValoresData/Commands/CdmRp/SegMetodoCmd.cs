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
    public class SegMetodoCmd : ISegMetodoCmd
    {
        private readonly DataBaseContext _context;
        public SegMetodoCmd(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SegMetodoModel>> GetMetodosAsync()
        {
            return await _context.V_SEG_METODO.ToListAsync();
        }
    }
}
