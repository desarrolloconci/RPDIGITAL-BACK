using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos.IndicacionesDto;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndicacionesController : ControllerBase
    {
        private readonly IIndicacionesService _indicacionesService;
        private readonly IConfiguration _configuration;

        public IndicacionesController(IIndicacionesService indicacionesService, IConfiguration configuration)
        {
            _indicacionesService = indicacionesService;
            _configuration = configuration;
        }

        // Epic 3: tab Indicaciones y Recetas (Fase 3 de la traza). Agrupa 7 SPs de listado en
        // paralelo + enriquecimiento Prestador/Usuario/Especialidad de las indicaciones con
        // dedupe y concurrencia limitada.
        [HttpGet("CargaInicial")]
        public async Task<ActionResult<CargaInicialIndicacionesDto>> GetCargaInicial(
            [FromQuery] int fichaId,
            [FromQuery] DateTime fechaDesde,
            [FromQuery] DateTime fechaHasta,
            [FromQuery] string area = "D",
            [FromQuery] int? usuarioIdQueConsulta = null,
            [FromQuery] string confCod = "Evolucion",
            [FromQuery] int? maxConcurrencia = null)
        {
            var concurrencia = maxConcurrencia
                ?? _configuration.GetValue<int?>("GeclisaParalelismo:MaxConcurrencia")
                ?? 6;

            var result = await _indicacionesService.GetCargaInicialAsync(
                fichaId, fechaDesde, fechaHasta, area, usuarioIdQueConsulta, confCod, concurrencia);

            return Ok(result);
        }

        // Epic 6: detalle de receta (click sobre una receta puntual). Responsabilidad del
        // frontend: 1 click = 1 sola llamada (ver traza original, Fase 9 - se llamaba 2 veces
        // con el mismo RecEnca_id).
        [HttpGet("Recetas/{recEncaId}/Detalle")]
        public async Task<ActionResult<IEnumerable<RecetaItemDto>>> GetDetalleReceta(int recEncaId)
        {
            var result = await _indicacionesService.GetDetalleRecetaAsync(recEncaId);

            return Ok(result);
        }
    }
}
