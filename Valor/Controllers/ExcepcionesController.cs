using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;
using ValorModels.Models;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcepcionesController : ControllerBase
    {
        private readonly IExcepcionesService _excepcionesService;

        public ExcepcionesController(IExcepcionesService excepcionesService)
        {
            _excepcionesService = excepcionesService;
        }

        [HttpGet]
       // [Authorize]
        public async Task<IEnumerable<ExcepcionesDto>> GetExcepcionesAsync()
        {
            return await _excepcionesService.GetExcepcionesAsync();
        }

        [HttpGet("{id}")] 
       // [Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult<ExcepcionesModel>> GetExcepcionesAsyncById(int id)
        {
            var excepcion = await _excepcionesService.GetExcepcionesDetailsAsync(id);

            if (excepcion == null)
            {
                return NotFound("Valor no encontrado");
            }

            return Ok(excepcion);
        }

        [HttpGet("byOs/{os}")]
        //[Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult<ExcepcionesModel>> GetExcepcionesAsyncByOS(int os)
        {
            var excepcion = await _excepcionesService.GetExcepcionesDetailsByOsAsync(os);

            if (excepcion == null)
            {
                return NotFound("Valor no encontrado");
            }

            return Ok(excepcion);
        }
        [HttpGet("byprograma/{os}/{prog}")]
       // [Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult<ExcepcionesDto>> GetExcepcionesDetailsByOsProgramaAsync(int os, int prog)
        {
            var excepcion = await _excepcionesService.GetExcepcionesDetailsByOsProgramaAsync(os, prog);

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
            var result = await _excepcionesService.DeleteExcepcionAsync(id);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }

        [HttpPost]
      //  [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> InsertExcepcionesAsync([FromBody] ExcepcionesModel excepcion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo inválido o ID no coincide");
            }
          

            var result = await _excepcionesService.InsertExcepcionAsync(excepcion);
            if (result == false)
            {
                return BadRequest("Obra solcial Repetida");
            }

            return CreatedAtAction(nameof(GetExcepcionesAsyncById), new { id = excepcion.id }, excepcion);
        
        }

        [HttpPut]
      //  [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> UpdateExcepcionesAsync([FromBody] ExcepcionesModel excepcion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo Invalido");
            }
            try
            {
                bool updateSuccessful = await _excepcionesService.UpdateExcepcionAsync(excepcion);
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





