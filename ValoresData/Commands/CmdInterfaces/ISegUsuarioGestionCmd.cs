using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface ISegUsuarioGestionCmd
    {
        public Task<bool> InsertSegUsuarioGestionAsync(SegUsuarioGestionModel model);
        public Task<bool> InsertSegUsuarioGestionVarios(SegUsuarioGestionModel model);
        public Task<bool> UpdateSegUsuarioGestionAsync(SegUsuarioGestionModel model);
        public Task<bool> UpdateSegUsuarioGestionVarios(SegUsuarioGestionModel model);
        public Task<IEnumerable<SegUsuarioGestionModel>> GetSegUsuarioGestion(SegUsuarioGestionModel model);
    }
}
