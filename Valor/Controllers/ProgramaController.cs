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
    public class ProgramaController : ControllerBase
    {
        private readonly IProgramaService _programaService;
        public ProgramaController(IProgramaService programaService)
        {
            _programaService = programaService;
        }

        [HttpGet]
      //  [Authorize]
        public async Task<IEnumerable<ProgramasModel>> GetPrograma()
        {
            return await _programaService.GetProgramaAsync();
        }

        [HttpPost]
       // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> InsertPrograma(ProgramasModel programas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo inválido o ID no coincide");
            }
            try
            {
                await _programaService.InsertProgramasync(programas);

                return CreatedAtAction("", new { message = "Registro Insertado", success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al insertar la práctica", success = false, error = ex.Message });
            }
        }
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,Supervisor")]
        public async Task<ActionResult<ProgramasModel>> GetValorModel(int id)
        {
            var ProgramasModel = await _programaService.GetProgramaDetailAsync(id); 

            if (ProgramasModel == null)
            {
                return NotFound("valor no encontrado");
            }

            return ProgramasModel;
        }

        [HttpPut]
        // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> UpdateProgramaAsync(ProgramasModel programas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo Invalido");
            }
            try
            {
                bool updateSuccessful = await _programaService.UpdateProgramaAsync(programas);
                if (updateSuccessful)
                {
                    return Ok(new { message = "Registro Actualizado", programas, success = true });

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
        [HttpDelete]
      // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> DeleteValor(int id)
        {
            if (id <= 0)
            {
                return BadRequest("id invalido");
            }
            var result = await _programaService.DeleteProgramaAsync(id);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }
    }
}
