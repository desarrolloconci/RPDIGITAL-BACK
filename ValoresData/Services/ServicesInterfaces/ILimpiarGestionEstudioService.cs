using System.Threading.Tasks;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface ILimpiarGestionEstudioService
    {
        public Task LimpiarGestionAsync(string idPedido, string idEstudio, string? usuario);
    }
}
