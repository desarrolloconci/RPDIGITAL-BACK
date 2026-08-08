using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos.ModulosDto;

namespace ValoresData.Services
{
    public class ModulosService : IModulosService
    {
        private readonly IModulosCmd _modulosCmd;

        public ModulosService(IModulosCmd modulosCmd)
        {
            _modulosCmd = modulosCmd;
        }

        public async Task<IEnumerable<ModuloDetalleDto>> GetDetalleModuloAsync(int meId)
        {
            return await _modulosCmd.GetDetalleModuloAsync(meId);
        }
    }
}
