using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;
using ValorModels.Models;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IExcepcionesCmd
    {
        public Task<IEnumerable<ExcepcionesDto>> GetExcepcionesAsync();
        public Task<ExcepcionesModel> GetExcepcionesDetailsAsync(int id);
        public Task<bool> InsertExcepcionAsync(ExcepcionesModel excepciones);
        public Task<bool> UpdateExcepcionAsync(ExcepcionesModel excepciones);
        public Task<bool> DeleteExcepcionAsync(int id);
        public Task<ExcepcionesModel> GetExcepcionesDetailsByOsAsync(int os);
        public Task<ExcepcionesDto> GetExcepcionesDetailsByOsProgramaAsync(int os, int programa_id);
    }
}
