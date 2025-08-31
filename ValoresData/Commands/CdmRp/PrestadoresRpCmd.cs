using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CdmRp
{
    public class PrestadoresRpCmd:IPrestadoresRpCmd
    {
        private readonly DataBaseContext _dbContext;
        public PrestadoresRpCmd(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PrestadoresRpModel>> GePrestadoresRpAsync()
        {
           return await _dbContext.V_PRESTADORES_RP.ToListAsync();
        }
    }
}
