using ValorModels.Dtos.IndicacionesDto;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IIndicacionesCmd
    {
        Task<CargaInicialIndicacionesDto> GetCargaInicialAsync(
            int fichaId, DateTime fechaDesde, DateTime fechaHasta, string area,
            int? usuarioIdQueConsulta, string confCod, int maxConcurrencia);

        Task<IEnumerable<RecetaItemDto>> GetDetalleRecetaAsync(int recEncaId);
    }
}
