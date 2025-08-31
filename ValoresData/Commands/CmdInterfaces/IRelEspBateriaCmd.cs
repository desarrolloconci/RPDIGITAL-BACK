using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IRelEspBateriaCmd
    {
        public Task<IEnumerable<RelEspBateriasModel>> GetRelEspServiciosAsync();

        public Task<IEnumerable<BhBateriasModel>> GetRelSolPractBhBateriaAsync(int usuario_id);
        public Task<bool> InsertRelEspBaterias(RelBateriasEspModel model);
        public Task<bool> DeleteRelEspBateriasAsync(int id);
    }
}
