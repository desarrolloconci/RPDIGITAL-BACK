using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface ISegMetodoCmd
    {
        public Task<IEnumerable<SegMetodoModel>> GetMetodosAsync();
    }
}
