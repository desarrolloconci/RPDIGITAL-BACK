using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IBhEstudiosCmd
    {
        public Task<IEnumerable<BhEstudiosModel>> GetBhEstudiosAsync(string? search);
    }
}
