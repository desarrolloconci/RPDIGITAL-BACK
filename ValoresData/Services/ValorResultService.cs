using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;

namespace ValoresData.Services
{
    public class ValorResultService: IValorResutlService
    {
        private readonly IValorResultCmd _valorResultCmd;
        public ValorResultService(IValorResultCmd valorResult)
        {
            _valorResultCmd = valorResult;
        }
        public async Task<IEnumerable<ValoresResultDto>> GetValorResultAsync(int idPrograma, int codigoOS, int planId)
        {
            var practicas = await _valorResultCmd.GetValorResultAsync(idPrograma, codigoOS, planId);
            foreach (var practica in practicas)
            {
                if (practica.TotalConvenio == 0)
                {
                    practica.Coseguro = 0;
                    practica.ValorParticular = await _valorResultCmd.GetPracticaParticularAsyncById(practica.CodigoPractica);

                }

            }

            return practicas;
            
        }
    }
}
