using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.RpInterfaces;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Services.RpServices
{
    public class RelEspServiciosServices : IRelEspeServiciosServices
    {
        private readonly IRelEspServiciosCmd _relEspServiciosCmd;
        public RelEspServiciosServices(IRelEspServiciosCmd relEspServiciosCmd)
        {
            _relEspServiciosCmd = relEspServiciosCmd;
        }
        public async Task<IEnumerable<RelEspServiciosModel>> GetRelEspServiciosAsync()
        {
            return await _relEspServiciosCmd.GetRelEspServiciosAsync();
        }

        public async Task<IEnumerable<RelSolPractBhServicioSolModel>> GetRelSolPractBhServicioSolAsync(int usuario_id)
        {
            return await _relEspServiciosCmd.GetRelSolPractBhServicioSolAsync(usuario_id);
        }
    }
}
