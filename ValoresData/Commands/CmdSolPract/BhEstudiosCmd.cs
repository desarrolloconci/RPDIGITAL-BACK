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

        public async Task<IEnumerable<BhEstudiosModel>> GetBhEstudiosAsync(string? search)
        {
            var query = _dbContext.V_BH_ESTUDIOS.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e => e.ESTUDIO_NOMBRE.Contains(search) || e.ESTUDIO_CODIGO.Contains(search));
            }

            return await query.OrderBy(e => e.ESTUDIO_NOMBRE).ToListAsync();
        }
    }
}
