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
    public class AtencionesPacienteService : IAtencionesPacienteService
    {
        private readonly IAtencionesPacienteCmd _atencionesPacienteCmd;
        public AtencionesPacienteService(IAtencionesPacienteCmd atencionesPacienteCmd)
        {
            _atencionesPacienteCmd = atencionesPacienteCmd;
        }

        public async Task<IEnumerable<AtencionPacienteModel>> GetAtencionesPacienteAsync(string dni, int dias = 60)
        {
            return await _atencionesPacienteCmd.GetAtencionesPacienteAsync(dni, dias);
        }
    }
}
