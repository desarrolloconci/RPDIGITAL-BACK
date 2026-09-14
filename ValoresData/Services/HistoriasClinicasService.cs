using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos.HistoriasClinicasDto;

namespace ValoresData.Services
{
    public class HistoriasClinicasService : IHistoriasClinicasService
    {
        private readonly IHistoriasClinicasCmd _historiasClinicasCmd;

        public HistoriasClinicasService(IHistoriasClinicasCmd historiasClinicasCmd)
        {
            _historiasClinicasCmd = historiasClinicasCmd;
        }

        public async Task<CargaInicialHistoriaClinicaDto> GetCargaInicialAsync(
            int fichaId, DateTime? fechaInicio, DateTime? fechaFin, string? tipoHc,
            string? tevCod, int? preId, int? panId, int? usuarioIdQueConsulta)
        {
            return await _historiasClinicasCmd.GetCargaInicialAsync(
                fichaId, fechaInicio, fechaFin, tipoHc, tevCod, preId, panId, usuarioIdQueConsulta);
        }

        public async Task<NotaSeleccionadaDetalleDto> GetDetalleNotaAsync(int hcId)
        {
            return await _historiasClinicasCmd.GetDetalleNotaAsync(hcId);
        }

        public async Task<UsuarioGeclisaDto?> GetUsuarioLogueadoAsync(int usuarioId)
        {
            return await _historiasClinicasCmd.GetUsuarioLogueadoAsync(usuarioId);
        }

        public async Task<AltaEvolucionResponseDto> InsertarEvolucionAsync(AltaEvolucionRequestDto request)
        {
            return await _historiasClinicasCmd.InsertarEvolucionAsync(request);
        }

        public async Task<IEnumerable<EstudioDto>> GetEstudiosAsync(int fichaId, DateTime fechaDesde, DateTime fechaHasta, string meArea)
        {
            return await _historiasClinicasCmd.GetEstudiosAsync(fichaId, fechaDesde, fechaHasta, meArea);
        }

        public async Task<int?> GetFichaIdPorDniAsync(string dni)
        {
            return await _historiasClinicasCmd.GetFichaIdPorDniAsync(dni);
        }
    }
}
