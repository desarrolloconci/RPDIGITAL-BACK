using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models;

namespace ValoresData.Commands.CmdValor
{
    public class RolUserCmd : IRolUserCmd
    {
        private readonly DataBaseContext _context;
        public RolUserCmd(DataBaseContext context)
        {
                _context = context;
        }
        public async Task<IEnumerable<RolUserModel>> GetRolUserAsync()
        {
            return await _context.RolUsers.ToListAsync();
        }
    }
}
