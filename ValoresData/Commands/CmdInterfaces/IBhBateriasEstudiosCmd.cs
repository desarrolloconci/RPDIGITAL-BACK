using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IBhBateriasEstudiosCmd
    {
        public Task<IEnumerable<BhBateriasModel>> GeBhBateriasEstudiosAsync();
        public Task<IEnumerable<BhBateriasEstudiosModel>> GetBhBateriasEstudiosGrupoAsync(int GrupoId);
    }
}
