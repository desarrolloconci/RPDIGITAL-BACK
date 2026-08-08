using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models.RpModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogCambiosRpController : ControllerBase
    {
        private readonly ILogCambiosRpService _service;
        public LogCambiosRpController(ILogCambiosRpService service)
        {
            _service = service;
        }

        [HttpGet("{idPedido}")]
        public async Task<ActionResult<IEnumerable<LogCambioRpModel>>> GetLogCambiosAsync(string idPedido, [FromQuery] string? idEstudio = null)
        {
            var log = await _service.GetLogCambiosAsync(idPedido, idEstudio);
            return Ok(log);
        }
    }
}
