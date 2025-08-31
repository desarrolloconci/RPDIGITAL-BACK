using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhInterfaces
{
    public interface IBhBateriasEstudiosService
    {
        public Task<IEnumerable<BhBateriasModel>> GeBhBateriasEstudiosAsync();
        public Task<IEnumerable<BhBateriasEstudiosModel>> GetBhBateriasEstudiosGrupoAsync(int GrupoId);
    }
}
