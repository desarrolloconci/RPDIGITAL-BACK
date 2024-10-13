using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ValorModels.Dtos;
using ValorModels.Models;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IPracticasCmd
    {
        public Task<IEnumerable<PracticasModel>> GetPracticasAsync();
        public Task<PracticasModel> GetPracticaAsyncById(int id);
        public Task<bool> InsertPracticaAsync(PracticasModel Practica);
        public Task<bool> UpdatePracticaAsync(PracticasModel Practica);
        public Task<bool> DeletePracticaAsync(int id);
        public Task<IEnumerable<PracticasDto>> GetPracticasDtoAsync();

    }
}
