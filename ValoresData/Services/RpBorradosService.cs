using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models.BhModels;

namespace ValoresData.Services
{
    public class RpBorradosService : IRpBorradosService
    {
        private readonly IRpBorradosCmd _cmd;
        public RpBorradosService(IRpBorradosCmd cmd)
        {
            _cmd = cmd;
        }

        public async Task<IEnumerable<RpBorradoModel>> GetRpBorradosAsync(string? dni = null, string? startFecha = null, string? endFecha = null, string? usuarioBorro = null, string? prestador = null)
        {
            return await _cmd.GetRpBorradosAsync(dni, startFecha, endFecha, usuarioBorro, prestador);
        }

        public async Task<IEnumerable<string>> GetUsuariosBorradoAsync()
        {
            return await _cmd.GetUsuariosBorradoAsync();
        }

        public async Task<IEnumerable<string>> GetPrestadoresBorradoAsync()
        {
            return await _cmd.GetPrestadoresBorradoAsync();
        }
    }
}
