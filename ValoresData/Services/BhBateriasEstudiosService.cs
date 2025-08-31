using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models.BhModels;

namespace ValoresData.Services
{
    public class BhBateriasEstudiosService : IBhBateriasEstudiosService
    {
        private readonly IBhBateriasEstudiosCmd _bh;
        public BhBateriasEstudiosService(IBhBateriasEstudiosCmd bh)
        {
            _bh = bh;
        }
        public async Task<IEnumerable<BhBateriasModel>> GeBhBateriasEstudiosAsync()
        {
            return await _bh.GeBhBateriasEstudiosAsync();
        }

        public async Task<IEnumerable<BhBateriasEstudiosModel>> GetBhBateriasEstudiosGrupoAsync(int GrupoId)
        {
            return await _bh.GetBhBateriasEstudiosGrupoAsync(GrupoId);
        }
    }
}
