using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos.RpDto;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CmdInterfaces
{
   public interface ISegCantContactosCmd
    {
        public Task<bool> InsertSegCantContactosAsync(SegCantContactosModel model);
        public Task<bool> InsertSegCantContactotVarios(SegCantContactosModel model);
        public Task<bool> DeleteSegCantContactoTotalAsync(SegCantContactosModel model, string? usuario = null);
        public Task<bool> DeleteSegCantContactoUnitarioAsync(SegCantContactosModel model, string? usuario = null);
        public Task<bool> UpdateSegCantidadContactosAsync(SegCantContactosModel model);
        public Task<bool> UpdateSegCantidadContactosVarios(SegCantContactosModel model);
        public Task<IEnumerable<SegCantContactosModel>> GetSegCantidadContactos(SegCantContactosModel model);
    }
}
