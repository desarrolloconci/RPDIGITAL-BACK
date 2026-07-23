using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IAtencionesPacienteCmd
    {
        public Task<IEnumerable<AtencionPacienteModel>> GetAtencionesPacienteAsync(string dni, int dias = 60);
    }
}
