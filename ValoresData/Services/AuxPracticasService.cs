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
    public class AuxPracticasService : IAuxPracticasService
    {
        private readonly IAuxPracticasCmd _auxPracticasCmd;
        public AuxPracticasService(IAuxPracticasCmd auxPracticasCmd)
        {
            _auxPracticasCmd = auxPracticasCmd;
        }

        public async Task<IEnumerable<AuxPracticasModel>> GetAuxPracticasAsync()
        {
            return await _auxPracticasCmd.GetAuxPracticasAsync();
        }

        public async Task<AuxPracticasModel> GetAuxPracticasByCodNomAsync(string codnom)
        {
            return await _auxPracticasCmd.GetAuxPracticasByCodNomAsync(codnom);
        }
    }
}
