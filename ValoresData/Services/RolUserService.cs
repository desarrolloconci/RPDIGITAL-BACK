using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models;

namespace ValoresData.Services
{
    public class RolUserService : IRolUserService
    {
        private readonly IRolUserCmd _rolUserCmd;
        public RolUserService(IRolUserCmd rolUserCmd)
        {
            _rolUserCmd = rolUserCmd;   
        }
        public async Task<IEnumerable<RolUserModel>> GetRolUserAsync()
        {
            return await _rolUserCmd.GetRolUserAsync();
        }
    }
}
