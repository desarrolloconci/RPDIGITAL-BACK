using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface ISegGrupoGestionCmd
    {
        public Task<IEnumerable<SegGrupoGestionModel>> GetSegGrupoGestionAsync();
    }
}
