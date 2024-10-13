using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models;

namespace ValoresData.Services
{
    public class ProgramasService : IProgramaService
    {
        private readonly IProgramaCmd _programaCmd;
        public ProgramasService(IProgramaCmd programaCmd)
        {
            _programaCmd = programaCmd;
        }
        public async Task<bool> DeleteProgramaAsync(int id)
        {
            return await _programaCmd.DeleteProgramaAsync(id);
        }

        public async Task<IEnumerable<ProgramasModel>> GetProgramaAsync()
        {
            return await _programaCmd.GetProgramaAsync();
        }

        public async Task<ProgramasModel> GetProgramaDetailAsync(int id)
        {
            return await _programaCmd.GetProgramaDetailAsync(id);
        }

        public async Task<bool> InsertProgramasync(ProgramasModel programaAtencion)
        {
            return await _programaCmd.InsertProgramasync(programaAtencion);
        }

        public async Task<bool> UpdateProgramaAsync(ProgramasModel programa)
        {
            return await _programaCmd.UpdateProgramaAsync(programa);
        }
    }
}
