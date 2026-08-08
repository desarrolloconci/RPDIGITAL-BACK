using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos.IndicacionesDto;

namespace ValoresData.Services
{
    public class IndicacionesService : IIndicacionesService
    {
        private readonly IIndicacionesCmd _indicacionesCmd;

        public IndicacionesService(IIndicacionesCmd indicacionesCmd)
        {
            _indicacionesCmd = indicacionesCmd;
        }

        public async Task<CargaInicialIndicacionesDto> GetCargaInicialAsync(
            int fichaId, DateTime fechaDesde, DateTime fechaHasta, string area,
            int? usuarioIdQueConsulta, string confCod, int maxConcurrencia)
        {
            return await _indicacionesCmd.GetCargaInicialAsync(
                fichaId, fechaDesde, fechaHasta, area, usuarioIdQueConsulta, confCod, maxConcurrencia);
        }

        public async Task<IEnumerable<RecetaItemDto>> GetDetalleRecetaAsync(int recEncaId)
        {
            return await _indicacionesCmd.GetDetalleRecetaAsync(recEncaId);
        }
    }
}
