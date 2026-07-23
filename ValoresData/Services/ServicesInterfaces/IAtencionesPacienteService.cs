using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IAtencionesPacienteService
    {
        public Task<IEnumerable<AtencionPacienteModel>> GetAtencionesPacienteAsync(string dni, int dias = 60);
    }
}
