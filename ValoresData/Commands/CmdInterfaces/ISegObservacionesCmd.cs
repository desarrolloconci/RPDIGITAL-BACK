using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CmdInterfaces
{
   public interface ISegObservacionesCmd
    {
        public Task<IEnumerable<SegObservacionesModel>> GetSegObservacionesAsync();

        public Task<bool> InsertSegObservacionesAsync(SegObservacionesModel model);
        public Task<bool> InsertSegObservacionesVarios(SegObservacionesModel model);
        public Task<IEnumerable<SegObservacionesModel>> GetSegObservacionesById(SegObservacionesModel model);
        public Task<bool> UpdateSegObservacionesAsync(SegObservacionesModel model);
        public Task<bool> UpdateSegObservacionesVarios(SegObservacionesModel model);
    }
}
