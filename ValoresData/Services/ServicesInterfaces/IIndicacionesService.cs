using ValorModels.Dtos.IndicacionesDto;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IIndicacionesService
    {
        Task<CargaInicialIndicacionesDto> GetCargaInicialAsync(
            int fichaId, DateTime fechaDesde, DateTime fechaHasta, string area,
            int? usuarioIdQueConsulta, string confCod, int maxConcurrencia);

        Task<IEnumerable<RecetaItemDto>> GetDetalleRecetaAsync(int recEncaId);
    }
}
