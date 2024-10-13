using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;
using ValorModels.Models;

namespace ValoresData.Services
{
    public class ExcepcionesService : IExcepcionesService
    {
        private readonly IExcepcionesCmd _excepcionesCmd;
        public ExcepcionesService(IExcepcionesCmd excepciones)
        {
            _excepcionesCmd= excepciones;
        }
        public async Task<bool> DeleteExcepcionAsync(int id)
        {
            return await _excepcionesCmd.DeleteExcepcionAsync(id);
        }

        public async Task<IEnumerable<ExcepcionesDto>> GetExcepcionesAsync()
        {
            return await _excepcionesCmd.GetExcepcionesAsync();
        }

        public async Task<ExcepcionesModel> GetExcepcionesDetailsAsync(int id)
        {
            return await _excepcionesCmd.GetExcepcionesDetailsAsync(id);
        }

        public async Task<ExcepcionesModel> GetExcepcionesDetailsByOsAsync(int os)
        {
            return await _excepcionesCmd.GetExcepcionesDetailsByOsAsync(os);
        }

        public async Task<bool> InsertExcepcionAsync(ExcepcionesModel excepciones)
        {
           /* ExcepcionesModel os_repetido = await _excepcionesCmd.GetExcepcionesDetailsByOsAsync(excepciones.codigo_os);
            if (os_repetido is not null)
            {
                return false;
            }
           */
            return await _excepcionesCmd.InsertExcepcionAsync(excepciones);
        }

        public Task<ExcepcionesDto> GetExcepcionesDetailsByOsProgramaAsync(int os, int programa_id)
        {
            return _excepcionesCmd.GetExcepcionesDetailsByOsProgramaAsync(os, programa_id);
        }

        public async Task<bool> UpdateExcepcionAsync(ExcepcionesModel excepciones)
        {
            return await _excepcionesCmd.UpdateExcepcionAsync(excepciones);
        }
    }
}
