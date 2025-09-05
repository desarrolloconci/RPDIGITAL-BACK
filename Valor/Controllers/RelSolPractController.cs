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
        // [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> InsertRelSolPractAsync([FromBody] List<RelSolPractModel> relSolPractModels)
        {
            if (relSolPractModels == null || !relSolPractModels.Any())
            {
                return BadRequest(new { message = "El modelo está vacío o es inválido", success = false });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Uno o más modelos son inválidos",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)),
                    success = false
                });
            }
             try
           {
            
            foreach (var model in relSolPractModels)
            {
                await _solPractService.InsertRelSolPractAsync(model);
            }

            return Ok(new
            {
                message = "Registros insertados exitosamente",
                insertedRecords = relSolPractModels,
                success = true
            });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new
                {
                    message = "Ocurrió un error al procesar la solicitud",
                    error = ex,
                    success = false
                });
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


        [HttpDelete]
        // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> DeletRelSolPractTotalAsync(string idpedido, string metodoOK)
        {
       
            var result = await _solPractService.DeletRelSolPractTotalAsync(idpedido, metodoOK);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }
        [HttpDelete]
        [Route("/Unitario")]
        // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> DeletRelSolPractUnitarioAsync(string idEstudio, string idPedido)
        {
       
            var result = await _solPractService.DeletRelSolPractUnitarioAsync(idEstudio, idPedido);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }
        [HttpDelete]
        [Route("/segumientoRp")]
        // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> DeleteUnificadoRelSolPractAsync(RelSolPractModel model)
        {

            var result = await _solPractService.DeleteUnificadoRelSolPractAsync(model);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }
    }
}
