using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.ServicesInterfaces
{
    public class GrupoEstudiosService : IGrupoEstudiosService
    {
        private readonly IGrupoEstudiosCmd _grupoEstudiosCmd;
        public GrupoEstudiosService(IGrupoEstudiosCmd  grupoEstudiosCmd)
        {
            _grupoEstudiosCmd = grupoEstudiosCmd;
        }
        public async Task<IEnumerable<GrupoEstudiosModel>> GetGrupoEstudiosAsync()
        {
            return await _grupoEstudiosCmd.GetGrupoEstudiosAsync();
        }
    }
}
