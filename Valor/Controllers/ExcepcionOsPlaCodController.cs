using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcepcionOsPlaCodController : ControllerBase
    {
        private readonly IExcepcionOsPlanCodService _excepcionOsPlanCodService;
        public ExcepcionOsPlaCodController(IExcepcionOsPlanCodService excepcionOsPlanCodService)
        {
            _excepcionOsPlanCodService = excepcionOsPlanCodService;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IEnumerable<ExcepcionOsPlanCodModel>> GetExcepcionesAsync()
        {
            return await _excepcionOsPlanCodService.GetExcepcionesAsync();
        }

        [HttpGet("{id}")]
      //  [Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult<ExcepcionOsPlanCodModel>> GetExcepcionesAsyncById(int id)
        {
            var excepcion = await _excepcionOsPlanCodService.GetExcepcionesDetailsAsync(id);

            if (excepcion == null)
            {
                return NotFound("Valor no encontrado");
            }

            return Ok(excepcion);
        }

        [HttpGet("byOs/{os}")]
       // [Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult<ExcepcionOsPlanCodModel>> GetExcepcionesAsyncByOS(int os)
        {
            var excepcion = await _excepcionOsPlanCodService.GetExcepcionesDetailsByOsAsync(os);

            if (excepcion == null)
            {
                return NotFound("Valor no encontrado");
            }

            return Ok(excepcion);
        }

        [HttpDelete("{id}")]
       // [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> DeleteExcepcionesAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("id invalido");
            }
            var result = await _excepcionOsPlanCodService.DeleteExcepcionAsync(id);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }
        [HttpGet("{id}/{plan}/{cod}")]
       // [Authorize]
        public async Task<ActionResult<ExcepcionOsPlanCodModel>> GetExcepcionesDetailsOSPlanPracAsync(int id, int plan, string cod)
        {
            var result = await _excepcionOsPlanCodService.GetExcepcionesDetailsOSPlanPracAsync(id, plan, cod);

            if (result == null)
            {
                return NotFound("valor no encontrado");
            }

            return result;
        }
        [HttpPost]
     //   [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> InsertExcepcionesAsync([FromBody] ExcepcionOsPlanCodModel excepcion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo inválido o ID no coincide");
            }

            await _excepcionOsPlanCodService.InsertExcepcionAsync(excepcion);

            return CreatedAtAction(nameof(GetExcepcionesAsyncById), new { id = excepcion.id }, excepcion);
        }

        [HttpPut]
       // [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> UpdateExcepcionesAsync([FromBody] ExcepcionOsPlanCodModel excepcion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo Invalido");
            }
            try
            {
                bool updateSuccessful = await _excepcionOsPlanCodService.UpdateExcepcionAsync(excepcion);
                if (updateSuccessful)
                {
                    return Ok(new { message = "Registro Actualizado", excepcion, success = true });

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
    }
}
