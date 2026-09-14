using ValorModels.Dtos.HistoriasClinicasDto;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IHistoriasClinicasService
    {
        Task<CargaInicialHistoriaClinicaDto> GetCargaInicialAsync(
            int fichaId, DateTime? fechaInicio, DateTime? fechaFin, string? tipoHc,
            string? tevCod, int? preId, int? panId, int? usuarioIdQueConsulta);

        Task<NotaSeleccionadaDetalleDto> GetDetalleNotaAsync(int hcId);

        Task<UsuarioGeclisaDto?> GetUsuarioLogueadoAsync(int usuarioId);

        Task<AltaEvolucionResponseDto> InsertarEvolucionAsync(AltaEvolucionRequestDto request);

        Task<IEnumerable<EstudioDto>> GetEstudiosAsync(int fichaId, DateTime fechaDesde, DateTime fechaHasta, string meArea);

        Task<int?> GetFichaIdPorDniAsync(string dni);
    }
}
