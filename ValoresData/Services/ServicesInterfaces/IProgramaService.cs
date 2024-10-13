using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IProgramaService
    {
        public Task<IEnumerable<ProgramasModel>> GetProgramaAsync();
        public Task<ProgramasModel> GetProgramaDetailAsync(int id);
        public Task<bool> InsertProgramasync(ProgramasModel programaAtencion);
        public Task<bool> UpdateProgramaAsync(ProgramasModel programaAtencion);
        public Task<bool> DeleteProgramaAsync(int id);
    }
}
