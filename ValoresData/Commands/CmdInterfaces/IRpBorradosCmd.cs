using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IRpBorradosCmd
    {
        public Task<IEnumerable<RpBorradoModel>> GetRpBorradosAsync(string? dni = null, string? startFecha = null, string? endFecha = null, string? usuarioBorro = null, string? prestador = null);
        public Task<IEnumerable<string>> GetUsuariosBorradoAsync();
        public Task<IEnumerable<string>> GetPrestadoresBorradoAsync();
    }
}
