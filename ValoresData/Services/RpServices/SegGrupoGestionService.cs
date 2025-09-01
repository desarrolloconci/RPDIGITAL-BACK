using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.RpInterfaces;
using ValorModels.Models.RpModels;

namespace ValoresData.Services.RpServices
{
    public class SegGrupoGestionService : ISegGrupoGestionService
    {
        private readonly ISegGrupoGestionCmd _cmd;
        public SegGrupoGestionService(ISegGrupoGestionCmd cmd)
        {
            _cmd = cmd;
        }
        public async Task<IEnumerable<SegGrupoGestionModel>> GetSegGrupoGestionAsync()
        {
            return await _cmd.GetSegGrupoGestionAsync();
        }
    }
}
