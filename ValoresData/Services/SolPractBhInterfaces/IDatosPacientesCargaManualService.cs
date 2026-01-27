using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;

namespace ValoresData.Services.SolPractBhInterfaces
{
    public interface IDatosPacientesCargaManualService
    {
        public Task<IEnumerable<UltimoPedidoPorDniModel>> GetPacienteAsync(string dni);
    }
}
