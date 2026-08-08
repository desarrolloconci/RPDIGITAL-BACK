using ValorModels.Dtos.ModulosDto;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IModulosService
    {
        Task<IEnumerable<ModuloDetalleDto>> GetDetalleModuloAsync(int meId);
    }
}
