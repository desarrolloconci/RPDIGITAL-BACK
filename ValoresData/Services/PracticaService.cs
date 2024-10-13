using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Commands.CmdValor;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;
using ValorModels.Models;

namespace ValoresData.Services
{
    public class PracticaService:IPracticasService 
    {
        private readonly IPracticasCmd _practicaCmd;
        public PracticaService(IPracticasCmd practicaCmd)
        {
            _practicaCmd = practicaCmd;
        }

        public Task<bool> DeletePracticaAsync(int id)
        {
            return _practicaCmd.DeletePracticaAsync(id);
        }

        public Task<IEnumerable<PracticasModel>> GetPracticaAsync()
        {
            return _practicaCmd.GetPracticasAsync();
        }
        public Task<PracticasModel> GetPracticaAsyncById(int id)
        {
            return _practicaCmd.GetPracticaAsyncById(id);
        }

        public async Task<bool> InsertPracticaAsync(PracticasModel practica)
        {
            return await _practicaCmd.InsertPracticaAsync(practica);
        }

        public async Task<bool> UpdatePracticaAsync(PracticasModel practica)
        {
            return await _practicaCmd.UpdatePracticaAsync(practica);
        }
        public async Task<IEnumerable<PracticasDto>> GetPracticasDtoAsync()
        {
            return await _practicaCmd.GetPracticasDtoAsync();
        }
    }
}
