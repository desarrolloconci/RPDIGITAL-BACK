using ValorModels.Models.BhModels;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IRpBorradosService
    {
        public Task<IEnumerable<RpBorradoModel>> GetRpBorradosAsync(string? dni = null, string? startFecha = null, string? endFecha = null, string? usuarioBorro = null, string? prestador = null);
        public Task<IEnumerable<string>> GetUsuariosBorradoAsync();
        public Task<IEnumerable<string>> GetPrestadoresBorradoAsync();
    }
}
