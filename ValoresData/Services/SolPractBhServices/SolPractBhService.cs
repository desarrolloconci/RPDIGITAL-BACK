using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Dtos.BhDto;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhServices
{
    public class SolPractBhService : ISolPractBhService
    {
        private readonly ISolPractBhCmd _solPractBhCmd;
        public SolPractBhService(ISolPractBhCmd solPractBhCmd)
        {
            _solPractBhCmd = solPractBhCmd;
        }


        public async Task<IEnumerable<SolPractBhMetodoDto>> GetSolPractAsync(DateTime? fechaCreacionRP, string? startFechaRP, string? endFechaRP, List<string>? unidad, string? dni, string? metodo, string? prestador, string? estudio, int? estadoPrograma, List<int>? estadoTurno, string? usuario, string? servicio, string? obrasocial, string? ultimoContacto, int? inductor)
        {
            return await _solPractBhCmd.GetSolPractAsync(fechaCreacionRP, startFechaRP, endFechaRP, unidad, dni, metodo, prestador, estudio, estadoPrograma, estadoTurno, usuario, servicio, obrasocial, ultimoContacto, inductor);
        }
      
        public async Task<IEnumerable<SolPractBhDto>> GetSolPractAsyncDistinct(DateTime? fechaCreacionRP, string? startFechaRP, string? endFechaRP, List<string>? unidad, string? dni, string? metodo, string? prestador, string? estudio, int? estadoPrograma, List<int>? estadoTurno, string? usuario, string? servicio, string? obrasocial, string? ultimoContacto, int? inductor)
        {
            return await _solPractBhCmd.GetSolPractAsyncDistinct(fechaCreacionRP, startFechaRP, endFechaRP, unidad, dni, metodo, prestador, estudio, estadoPrograma, estadoTurno, usuario, servicio, obrasocial, ultimoContacto, inductor);
        }

        public async Task<IEnumerable<SolPractBhDto>> GetSolPractRpAsync(string? startFechaRP = null, string? endFechaRP = null, List<string>? unidad = null, string? dni = null, string? metodo = null, string? prestador = null, string? estudio = null, int? estadoPrograma = null, List<string>? estadoTurno = null, string? usuario = null, string? servicio = null, string? obrasocial = null, string? ultimoContacto = null, int? inductor = null, int? grupoGestionId = null)
        {
            return await _solPractBhCmd.GetSolPractRpAsync(startFechaRP, endFechaRP, unidad, dni, metodo, prestador, estudio, estadoPrograma, estadoTurno, usuario, servicio, obrasocial, ultimoContacto, inductor, grupoGestionId);
        }
        public async Task<IEnumerable<SolPractBhRpDto>> GetSolPractRpPdfAsync(string IDPEDIDO, string metodo)
        {
            return await _solPractBhCmd.GetSolPractRpPdfAsync(IDPEDIDO, metodo);
        }
        public async Task<bool> UpdateSolPractRpAsync(SolPractBhPedidoManualModel SolPract)
        {
            var Tipo_pedido = await _solPractBhCmd.GetSolPractUnionAsync(SolPract.IDPEDIDO);
            if (Tipo_pedido.origen == 1)
            {
                return await _solPractBhCmd.UpdateSolPractRpAsync(SolPract);
            }
            return await _solPractBhCmd.UpdateSolPractOldRpAsync(SolPract);
        }
        public async Task<IEnumerable<SolPractBhMetodoDto>> GetSolPractSeguimientoAsync(
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
        )
        { return await _solPractBhCmd.GetSolPractSeguimientoAsync(fechaCreacionRP, startFechaRP, endFechaRP, unidad, dni, metodo, prestador, estudio, estadoPrograma, estadoTurno, usuario, servicio, obrasocial, ultimoContacto, inductor);
        }

        public async Task<bool> DeleteSolPractTotalAsync(string idpedido)
        {
            return await _solPractBhCmd.DeleteSolPractTotalAsync(idpedido);
        }

        public async Task<IEnumerable<SolPractBhDto>> GetPedidosAnterioresPorDniAsync(string dni)
        {
            return await _solPractBhCmd.GetPedidosAnterioresPorDniAsync(dni);
        }

        public async Task<Dictionary<string, bool>> GetDnisConRpAsync(List<string> dnis)
        {
            var conRp = (await _solPractBhCmd.GetDnisConRpAsync(dnis)).ToHashSet();
            return dnis.Distinct().ToDictionary(dni => dni, dni => conRp.Contains(dni));
        }
    }
}
