using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionesPacienteController : ControllerBase
    {
        private readonly IAtencionesPacienteService _atencionesPacienteService;
        public AtencionesPacienteController(IAtencionesPacienteService atencionesPacienteService)
        {
            _atencionesPacienteService = atencionesPacienteService;
        }

        [HttpGet("{dni}")]
        public async Task<ActionResult<IEnumerable<AtencionPacienteModel>>> GetAtencionesPacienteAsync(string dni, [FromQuery] int dias = 60)
        {
            var atenciones = await _atencionesPacienteService.GetAtencionesPacienteAsync(dni, dias);
            return Ok(atenciones);
        }
    }
}
