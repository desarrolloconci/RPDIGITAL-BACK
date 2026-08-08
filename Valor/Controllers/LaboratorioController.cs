using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos.LaboratorioDto;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratorioController : ControllerBase
    {
        private readonly ILaboratorioService _laboratorioService;
        private readonly IConfiguration _configuration;

        public LaboratorioController(ILaboratorioService laboratorioService, IConfiguration configuration)
        {
            _laboratorioService = laboratorioService;
            _configuration = configuration;
        }

        // Epic 2: tab Laboratorio. Reemplaza pa_HistoriaPrestacional_LaboratorioResultado (1 llamada)
        // + pa_HistoriaPrestacional_LaboratorioResultadoValorxDet (antes N llamadas secuenciales,
        // 63 en la traza original) por 1 endpoint que dispara el segundo SP en paralelo con
        // concurrencia limitada. maxConcurrencia es override manual solo para el test de carga
        // de la Estrategia de Validacion (5/10/15/20) - en uso normal se deja el default configurado.
        [HttpGet("Completo")]
        public async Task<ActionResult<LaboratorioCompletoDto>> GetCompleto(
            [FromQuery] int fichaId,
            [FromQuery] DateTime fechaDesde,
            [FromQuery] DateTime fechaHasta,
            [FromQuery] string meArea = "D",
            [FromQuery] bool soloValidado = false,
            [FromQuery] int? asId = null,
            [FromQuery] int? maxConcurrencia = null)
        {
            var concurrencia = maxConcurrencia
                ?? _configuration.GetValue<int?>("GeclisaParalelismo:MaxConcurrencia")
                ?? 6;

            var result = await _laboratorioService.GetLaboratorioCompletoAsync(
                fichaId, fechaDesde, fechaHasta, meArea, soloValidado, asId, concurrencia);

            return Ok(result);
        }
    }
}
