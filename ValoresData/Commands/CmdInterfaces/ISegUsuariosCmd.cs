using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface ISegUsuariosCmd
    {
        public  Task<IEnumerable<SegUsuariosModel>> GetSegUsuariosAsync();
    }
}
