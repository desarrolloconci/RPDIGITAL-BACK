using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Dtos.BhDto;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhServices
{
    public class SolPractBhService : ISolPractBhService
    {
        private readonly ISolPractBhCmd _solPractBhCmd;
        public SolPractBhService(ISolPractBhCmd solPractBhCmd)
        {
            _solPractBhCmd = solPractBhCmd;
        }



        public async Task<IEnumerable<SolPractBhDto>> GetSolPractAsync(DateTime? fechaCreacionRP = null, string unidad = null, string dni = null, string metodo = null, string prestador = null, string estudio = null, string estadoPractica = null)
        {
            return await _solPractBhCmd.GetSolPractAsync(fechaCreacionRP, unidad, dni, metodo, prestador, estudio, estadoPractica);
        }

        public async Task<IEnumerable<SolPractBhDto>> GetSolPractAsyncDistinct(DateTime? fechaCreacionRP = null, string unidad = null, string dni = null, string metodo = null, string prestador = null, string estudio = null, string estadoPractica = null)
        {
            return await _solPractBhCmd.GetSolPractAsyncDistinct(fechaCreacionRP, unidad, dni, metodo, prestador, estudio, estadoPractica);
        }
    }
}
