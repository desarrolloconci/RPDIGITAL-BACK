using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models.RpModels;

namespace ValoresData.Services
{
    // Limpia todo lo relacionado al seguimiento de un estudio puntual (turno, motivo de no turno,
    // observaciones y cantidad de contactos) sin tocar el pedido en si: se usa para dejar un estudio
    // "como recien creado" cuando la gestion se hizo mal, en vez de borrar el pedido.
    public class LimpiarGestionEstudioService : ILimpiarGestionEstudioService
    {
        private readonly IReslSolPractCmd _relSolPractCmd;
        private readonly IRelSegMotivoNoTurnoCmd _relSegMotivoNoTurnoCmd;
        private readonly ISegObservacionesCmd _segObservacionesCmd;
        private readonly ISegCantContactosCmd _segCantContactosCmd;

        public LimpiarGestionEstudioService(
            IReslSolPractCmd relSolPractCmd,
            IRelSegMotivoNoTurnoCmd relSegMotivoNoTurnoCmd,
            ISegObservacionesCmd segObservacionesCmd,
            ISegCantContactosCmd segCantContactosCmd)
        {
            _relSolPractCmd = relSolPractCmd;
            _relSegMotivoNoTurnoCmd = relSegMotivoNoTurnoCmd;
            _segObservacionesCmd = segObservacionesCmd;
            _segCantContactosCmd = segCantContactosCmd;
        }

        public async Task LimpiarGestionAsync(string idPedido, string idEstudio, string? usuario)
        {
            await _relSolPractCmd.DeletRelSolPractUnitarioAsync(idEstudio, idPedido, usuario);
            await _relSegMotivoNoTurnoCmd.DeleteRelSegMotivoNoTurnoAsync(idPedido, idEstudio, usuario);
            await _segObservacionesCmd.DeleteSegObservacionesAsync(idPedido, idEstudio, usuario);
            await _segCantContactosCmd.DeleteSegCantContactoUnitarioAsync(
                new SegCantContactosModel { idPedido = idPedido, idEstudio = idEstudio }, usuario);
        }
    }
}
