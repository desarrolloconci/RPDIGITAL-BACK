using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;
using ValorModels.Models;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IPracticasService
    {
        public Task<IEnumerable<PracticasModel>> GetPracticaAsync();
        public Task<PracticasModel> GetPracticaAsyncById(int id);
        public Task<bool> DeletePracticaAsync(int id);
        public Task<bool> InsertPracticaAsync(PracticasModel practica);
        public Task<bool> UpdatePracticaAsync(PracticasModel practica);
        public Task<IEnumerable<PracticasDto>> GetPracticasDtoAsync();
    }
}
