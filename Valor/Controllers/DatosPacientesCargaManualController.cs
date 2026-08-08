using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosPacientesCargaManualController : ControllerBase
    {
        private readonly IDatosPacientesCargaManualService _service;
        public DatosPacientesCargaManualController(IDatosPacientesCargaManualService service)
        {
            _service = service;
        }

        [HttpGet("{dni}")]
       // [Authorize]
        public async Task<ActionResult<UltimoPedidoPorDniModel>> GetPacienteAsync(string dni)
        {
            var Paciente = await _service.GetPacienteAsync(dni);
            if (Paciente == null)
            {
                return NotFound("Valor no encontrado");
            }
            return Ok(Paciente);
        }
    } 
}
