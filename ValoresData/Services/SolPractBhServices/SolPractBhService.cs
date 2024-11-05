using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Dtos.BhDto;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhServices
{
    public class SolPractBhService : ISolPractBhService
    {
        private readonly ISolPractBhCmd _solPractBhCmd;
        public SolPractBhService(ISolPractBhCmd solPractBhCmd)
        {
            _solPractBhCmd = solPractBhCmd;
        }


        public async Task<IEnumerable<SolPractBhDto>> GetSolPractAsync(DateTime? fechaCreacionRP, string? startFechaRP, string? endFechaRP, string? unidad, string? dni, string? metodo, string? prestador, string? estudio, string? estadoPractica,string? estadoTurno , string? usuario, string? servicio, string? obrasocial, string? ultimoContacto)
        {
            return await _solPractBhCmd.GetSolPractAsync(fechaCreacionRP, startFechaRP, endFechaRP, unidad, dni, metodo, prestador, estudio, estadoPractica,estadoTurno, usuario, servicio, obrasocial, ultimoContacto);
        }


        public async Task<IEnumerable<SolPractBhDto>> GetSolPractAsyncDistinct(DateTime? fechaCreacionRP, string? startFechaRP, string? endFechaRP, string? unidad, string? dni, string? metodo, string? prestador, string? estudio, string? estadoPractica, string? estadoTurno, string? usuario, string? servicio, string? obrasocial, string? ultimoContacto)
        {
            return await _solPractBhCmd.GetSolPractAsyncDistinct(fechaCreacionRP, startFechaRP, endFechaRP, unidad, dni, metodo, prestador, estudio, estadoPractica,estadoTurno, usuario, servicio, obrasocial, ultimoContacto);
        }
    }
}
