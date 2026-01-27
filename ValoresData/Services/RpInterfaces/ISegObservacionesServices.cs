using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.RpModels;

namespace ValoresData.Services.RpInterfaces
{
   public interface ISegObservacionesServices
    {
        public Task<IEnumerable<SegObservacionesModel>> GetSegObservacionesAsync();

        public Task<bool> ManageSegObservacionesAsyncAsync(SegObservacionesModel model);
    }
}
