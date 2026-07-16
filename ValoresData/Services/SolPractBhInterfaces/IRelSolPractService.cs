using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhInterfaces
{
    public interface IRelSolPractService
    {
        public Task<IEnumerable<RelSolPractModel>> GetRelSolPractAsync();
        public Task<bool> InsertRelSolPractAsync(RelSolPractModel relSolPractModel);
        public Task<bool> UpdateRelSolAsync(RelSolPractModel relSolPractModel);
        public Task<RelSolPractModel> GetRelSolAsyncById(int id);
        public Task<bool> DeletRelSolPractTotalAsync(string idpedido, string metodoOK);
        public Task<bool> DeletRelSolPractUnitarioAsync(string idEstudio, string idPedido);
        public Task<bool> DeleteUnificadoRelSolPractAsync(SEG_DESASOCIOARTURNO_DTO model);
        public Task<IEnumerable<SolPractBhPedidoManualModel>> GetRpVinculadosATurnoAsync(int turnoId);
        public Task<Dictionary<int, bool>> GetTurnosConPedidoAsync(List<int> turnoIds);

    }
}
