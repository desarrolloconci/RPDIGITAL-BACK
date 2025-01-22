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
    public class BhEstudiosCmd:IBhEstudiosCmd
    { private readonly DataBaseContext _dbContext;
        public BhEstudiosCmd(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BhEstudiosModel>> GetBhEstudiosAsync()
        {
           return await _dbContext.V_BH_ESTUDIOS.ToListAsync();
        }
    }
}
