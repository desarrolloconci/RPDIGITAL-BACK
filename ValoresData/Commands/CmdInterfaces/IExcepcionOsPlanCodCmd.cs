using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IExcepcionOsPlanCodCmd
    {
        public Task<IEnumerable<ExcepcionOsPlanCodModel>> GetExcepcionesAsync();
        public Task<ExcepcionOsPlanCodModel> GetExcepcionesDetailsAsync(int id);
        public Task<ExcepcionOsPlanCodModel> GetExcepcionesDetailsOSPlanPracAsync(int os, int plan, string cod);
        public Task<bool> InsertExcepcionAsync(ExcepcionOsPlanCodModel excepciones);
        public Task<bool> UpdateExcepcionAsync(ExcepcionOsPlanCodModel excepciones);
        public Task<bool> DeleteExcepcionAsync(int id);
        public Task<ExcepcionOsPlanCodModel> GetExcepcionesDetailsByOsAsync(int os);
    }
}
