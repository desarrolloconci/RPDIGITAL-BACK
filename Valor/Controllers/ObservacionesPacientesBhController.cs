using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.SolPractBhInterfaces;
using ValoresData.Services.SolPractBhServices;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservacionesPacientesBhController : ControllerBase
    {
        private readonly IObservacionesPacientesBhService _iobservaciones;
        public ObservacionesPacientesBhController(IObservacionesPacientesBhService observacionesPacientesBhService)
        {
            _iobservaciones=observacionesPacientesBhService;
        }
       [HttpGet]
        public async Task<IEnumerable<ObservacionesPacientesBhModel>> GetObservacionesPacientesBhAsync()
        {
            return await _iobservaciones.GetObservacionesPacientesBhAsync();
        }
    
        [HttpPost]
        public async Task<IActionResult> ManageObservacionesPacientesBhsync(ObservacionesPacientesBhModel Observacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo inválido o ID no coincide");
            }
            try
            {
                await _iobservaciones.ManageObservacionesPacientesBhsync(Observacion);

                return Ok(new { message = "Registro Insertado", success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al insertar la práctica", success = false, error = ex.Message });
            }

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteObservacionesPacientesBhAsync(string dni,string unidad)
        {
            if (dni is null )
            {
                return BadRequest("id invalido");
            }
            var result = await _iobservaciones.DeleteObservacionesPacientesBhAsync(dni, unidad);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> UpdatObservacionesPacientesBhAsync(ObservacionesPacientesBhModel Observacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo Invalido");
            }
            try
            {
                bool updateSuccessful = await _iobservaciones.UpdatObservacionesPacientesBhAsync(Observacion);
                if (updateSuccessful)
                {
                    return Ok(new { message = "Registro Actualizado", Observacion, success = true });

                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Un error ocurrio durante la actualizacion.");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error Inesperado: {ex.Message}");
            }
          

        }
        [HttpGet("{dni}/{unidad}")]
        public async Task<ActionResult<ObservacionesPacientesBhModel>> GetObservacionesPacientesBhADetailAsync(string dni,string unidad)
        {
            var obs = await _iobservaciones.GetObservacionesPacientesBhADetailAsync(dni,unidad);
            if (obs == null)
            {
                return NotFound("valor no encontrado");
            }
            return obs;
        }
    }
}
