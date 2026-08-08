using ValorModels.Dtos.ModulosDto;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IModulosCmd
    {
        Task<IEnumerable<ModuloDetalleDto>> GetDetalleModuloAsync(int meId);
    }
}
