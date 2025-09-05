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
    public class SegUsuariosService : ISegUsuariosService
    {
        private readonly ISegUsuariosCmd _cmd;
        public SegUsuariosService(ISegUsuariosCmd cmd)
        {
            _cmd = cmd;
        }
        public async Task<IEnumerable<SegUsuariosModel>> GetSegUsuariosAsync()
        {
            return await _cmd.GetSegUsuariosAsync();
        }
    }
}
