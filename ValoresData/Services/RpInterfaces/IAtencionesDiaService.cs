using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos.RpDto;
using ValorModels.Models.RpModels;

namespace ValoresData.Services.RpInterfaces
{
    public interface IAtencionesDiaService
    {
        public Task<IEnumerable<AtencionesDiaModel>> GetAtencionesDiaAsync(int UserID);
        public Task<IEnumerable<ListadoServicioAtencionDto>> GetServicioAtencionesDiaAsync(int UserID);
        public  Task<IEnumerable<AtencionesDiaModel>> GetAtencionesDiaServicioAsync(int UserID, string servicio);
    }
}
