using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos.RpDto;
using ValorModels.Models.RpModels;

namespace ValoresData.Services.RpInterfaces
{
   public interface ISegCantContactosService
    {
        public Task<bool> InsertSegCantContactosAsync(SegCantContactosModel model);
        //public Task<bool> InserSegCantContactotVarios(SegCantContactosDto model);
        public  Task<bool> DeleteSegCantContactoTotalAsync(SegCantContactosModel model);
        //public Task<bool> DeleteSegCantContactoUnitarioAsync(string idEstudio, string idPedido);
    }
}
