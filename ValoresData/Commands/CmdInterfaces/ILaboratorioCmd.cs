using ValorModels.Dtos.LaboratorioDto;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface ILaboratorioCmd
    {
        Task<LaboratorioCompletoDto> GetLaboratorioCompletoAsync(
            int fichaId, DateTime fechaDesde, DateTime fechaHasta, string meArea,
            bool soloValidado, int? asId, int maxConcurrencia);
    }
}
