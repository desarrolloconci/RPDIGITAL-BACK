using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IReslSolPractCmd
    {
        public Task<IEnumerable<RelSolPractModel>> GetRelSolPractAsync();

        public Task<bool> InsertRelSolPractAsync(RelSolPractModel relSolPractModel);
        public Task<RelSolPractModel> GetRelSolAsyncById(int id);
        public Task<bool> UpdateRelSolAsync(RelSolPractModel relSolPractModel);
        public Task<bool> InsertRelSolPractAsyncVarios(RelSolPractModel relSolPractModel);
        public  Task<bool> DeletRelSolPractTotalAsync(string idpedido, string metodoOK);
        public Task<bool> DeletRelSolPractUnitarioAsync(string idEstudio, string idPedido);
        public Task<IEnumerable<RelSolPractModel>> GetRelSolVariosAsync(string idPedido, string idEstudio);
        public Task<IEnumerable<SolPractBhPedidoManualModel>> GetRpVinculadosATurnoAsync(int turnoId);
    }
}
