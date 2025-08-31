using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.RpInterfaces;
using ValorModels.Dtos.RpDto;
using ValorModels.Models.RpModels;

namespace ValoresData.Services.RpServices
{
    public class AtencionesDiaServices : IAtencionesDiaService
    {
        private readonly IAtencionesDialCmd _atenciones;
        public AtencionesDiaServices(IAtencionesDialCmd atenciones)
        {
            _atenciones = atenciones;
        }
        public async Task<IEnumerable<AtencionesDiaModel>> GetAtencionesDiaAsync(int UserID)
        {
            return await _atenciones.GetAtencionesDiaAsync(UserID);
        }
        public async Task<IEnumerable<ListadoServicioAtencionDto>> GetServicioAtencionesDiaAsync(int UserID)
        {
            return await _atenciones.GetServicioAtencionesDiaAsync(UserID);
        }
        public async Task<IEnumerable<AtencionesDiaModel>> GetAtencionesDiaServicioAsync(int UserID, string servicio)
        {
            return await _atenciones.GetAtencionesDiaServicioAsync(UserID, servicio);
        }
    }
}
