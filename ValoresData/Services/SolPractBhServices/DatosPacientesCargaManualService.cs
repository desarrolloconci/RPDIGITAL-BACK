using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models;

namespace ValoresData.Services.SolPractBhServices
{
    public class DatosPacientesCargaManualService : IDatosPacientesCargaManualService
    {
        private readonly IDatosPacientesCargaManualCmd _cmd;
        public DatosPacientesCargaManualService(IDatosPacientesCargaManualCmd cmd)
        {
            _cmd = cmd;
        }
        public async Task<IEnumerable<UltimoPedidoPorDniModel>> GetPacienteAsync(string dni)
        {
            return await _cmd.GetPacienteAsync(dni);
        }
    }
}
