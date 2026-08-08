using ValorModels.Dtos.LaboratorioDto;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface ILaboratorioService
    {
        Task<LaboratorioCompletoDto> GetLaboratorioCompletoAsync(
            int fichaId, DateTime fechaDesde, DateTime fechaHasta, string meArea,
            bool soloValidado, int? asId, int maxConcurrencia);
    }
}
