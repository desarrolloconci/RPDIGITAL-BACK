using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IRolUserCmd
    {
        public Task<IEnumerable<RolUserModel>> GetRolUserAsync();
    }
}
