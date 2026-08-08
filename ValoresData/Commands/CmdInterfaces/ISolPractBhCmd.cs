using ValorModels.Dtos.BhDto;
using ValorModels.Models;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface ISolPractBhCmd
    {
        public Task<IEnumerable<SolPractBhMetodoDto>> GetSolPractAsync(
            DateTime? fechaCreacionRP,
            string? startFechaRP,
            string? endFechaRP,
            List<string>? unidad,
            string? dni,
            string? metodo,
            string? prestador,
            string? estudio,
            int? estadoPrograma,
            List<int>? estadoTurno,
            string? usuario,
            string? servicio,
            string? obrasocial,
            string? ultimoContacto,
            int? inductor);

        public Task<IEnumerable<SolPractBhDto>> GetSolPractAsyncDistinct(
            DateTime? fechaCreacionRP,
            string? startFechaRP,
            string? endFechaRP,
            List<string>? unidad,
            string? dni,
            string? metodo,
            string? prestador,
            string? estudio,
            int? estadoPrograma,
            List<int>? estadoTurno,
            string? usuario,
            string? servicio,
            string? obrasocial,
            string? ultimoContacto,
            int? inductor);
        public Task<IEnumerable<SolPractBhDto>> GetSolPractRpAsync(
            string? startFechaRP = null,
            string? endFechaRP = null,
            List<string>? unidad = null,
            string? dni = null,
            string? metodo = null,
            string? prestador = null,
            string? estudio = null,
            int? estadoPrograma = null,
            List<string>? estadoTurno = null,
            string? usuario = null,
            string? servicio = null,
            string? obrasocial = null,
            string? ultimoContacto = null,
            int? inductor = null,
            int? grupoGestionId = null);



        public Task<IEnumerable<SolPractBhRpDto>> GetSolPractRpPdfAsync(string IDPEDIDO, string metodo);

        public Task<bool> UpdateSolPractRpAsync(SolPractBhPedidoManualModel SolPract);
        public Task<UnionSolPractModel> GetSolPractUnionAsync(string IDPEDIDO);
        public Task<bool> UpdateSolPractOldRpAsync(SolPractBhPedidoManualModel SolPract);
        public Task<IEnumerable<SolPractBhMetodoDto>> GetSolPractSeguimientoAsync(
               DateTime? fechaCreacionRP = null,
               string? startFechaRP = null,
               string? endFechaRP = null,
               List<string>? unidad = null,
               string? dni = null,
               string? metodo = null,
               string? prestador = null,
               string? estudio = null,
               int? estadoPrograma = null,
               List<int>? estadoTurno = null,
               string? usuario = null,
               string? servicio = null,
               string? obrasocial = null,
               string? ultimoContacto = null,
               int? inductor = null
              );
        public Task<IEnumerable<SolPractBhDto>> GetSolPractRpFilaAsync(string idPedido, string? idEstudio, string? metodo);
        public  Task<bool> DeleteSolPractTotalAsync(string idpedido, string? usuario = null, string? motivoBorrado = null);
        public Task<IEnumerable<SolPractBhDto>> GetPedidosAnterioresPorDniAsync(string dni);
        public Task<IEnumerable<string>> GetDnisConRpAsync(List<string> dnis);
    }


}
