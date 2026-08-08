using System.Collections.Generic;
using System.Threading.Tasks;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface ILogCambiosRpCmd
    {
        public Task RegistrarCambioAsync(
            string idPedido,
            string? idEstudio,
            string accion,
            string? campo,
            string? valorAnterior,
            string? valorNuevo,
            string? usuario,
            int? idTurno = null);

        public Task<IEnumerable<LogCambioRpModel>> GetLogCambiosAsync(string idPedido, string? idEstudio = null);
    }
}
