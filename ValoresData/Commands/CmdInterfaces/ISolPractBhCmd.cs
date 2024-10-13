using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ValorModels.Dtos.BhDto;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface ISolPractBhCmd
    {
        public Task<IEnumerable<SolPractBhDto>> GetSolPractAsync(
                DateTime? fechaCreacionRP = null,
               string unidad = null,
               //string metodo = null,
               string dni = null,
               string metodo = null,
               string prestador = null,
               string estudio = null,
               string estadoPractica = null);
        public Task<IEnumerable<SolPractBhDto>> GetSolPractAsyncDistinct(
                DateTime? fechaCreacionRP = null,
               string unidad = null,
               //string metodo = null,
               string dni = null,
               string metodo = null,
               string prestador = null,
               string estudio = null,
               string estadoPractica = null);
    }


}
