using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos.LaboratorioDto;

namespace ValoresData.Services
{
    public class LaboratorioService : ILaboratorioService
    {
        private readonly ILaboratorioCmd _laboratorioCmd;

        public LaboratorioService(ILaboratorioCmd laboratorioCmd)
        {
            _laboratorioCmd = laboratorioCmd;
        }

        public async Task<LaboratorioCompletoDto> GetLaboratorioCompletoAsync(
            int fichaId, DateTime fechaDesde, DateTime fechaHasta, string meArea,
            bool soloValidado, int? asId, int maxConcurrencia)
        {
            return await _laboratorioCmd.GetLaboratorioCompletoAsync(
                fichaId, fechaDesde, fechaHasta, meArea, soloValidado, asId, maxConcurrencia);
        }
    }
}
