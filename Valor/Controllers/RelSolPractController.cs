using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.SolPractBhInterfaces;
using ValoresData.Services.SolPractBhServices;
using ValorModels.Models;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelSolPractController : ControllerBase
    {
        private readonly IRelSolPractService _solPractService;
        public RelSolPractController(IRelSolPractService relSolPractService)
        {
            _solPractService = relSolPractService;
        }

        [HttpGet]

        public async Task<IEnumerable<RelSolPractModel>> GetSolPract()
        {

            return await _solPractService.GetRelSolPractAsync();
        }
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,Supervisor")]
        public async Task<ActionResult<RelSolPractModel>> GetSolPractById(int id)
        {
            var relSolPractModel = await _solPractService.GetRelSolAsyncById(id);

            if (relSolPractModel == null)
            {
                return NotFound("valor no encontrado");
            }

            return relSolPractModel;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> InsertProgramasync(RelSolPractModel relSolPractModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo inválido o ID no coincide");
            }
            try
            {
                await _solPractService.InsertProgramasync(relSolPractModel);

                return CreatedAtAction("", new { message = "Registro Insertado", success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al insertar la práctica", success = false, error = ex.Message });
            }

        }

        [HttpPut]
        // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> UpdateRelSolAsync(RelSolPractModel relSolPractModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo Invalido");
            }

            try
            {
                bool updateSuccessful = await _solPractService.UpdateRelSolAsync(relSolPractModel);

                if (updateSuccessful)
                {
                    return Ok(new { message = "Registro Actualizado",relSolPractModel, success = true });

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
