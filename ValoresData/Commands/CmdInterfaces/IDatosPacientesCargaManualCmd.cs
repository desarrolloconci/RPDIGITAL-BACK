using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdInterfaces
{
   public interface IDatosPacientesCargaManualCmd
    {

        public Task<IEnumerable<UltimoPedidoPorDniModel>> GetPacienteAsync(string dni);


    }
}
