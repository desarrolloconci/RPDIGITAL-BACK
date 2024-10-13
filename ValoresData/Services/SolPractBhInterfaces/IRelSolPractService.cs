using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhInterfaces
{
    public interface IRelSolPractService
    {
        public Task<IEnumerable<RelSolPractModel>> GetRelSolPractAsync();
        public Task<bool> InsertProgramasync(RelSolPractModel relSolPractModel);
        public Task<bool> UpdateRelSolAsync(RelSolPractModel relSolPractModel);
        public  Task<RelSolPractModel> GetRelSolAsyncById(int id);

    }
}
