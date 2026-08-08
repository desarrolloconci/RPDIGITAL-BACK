using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using ValoresData.Services.RpInterfaces;
using ValoresData.Services.RpServices;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelEspBateriaController : ControllerBase
    {
        private readonly IRelEspBateriaService _EspBateriaService;
        public RelEspBateriaController(IRelEspBateriaService relEspBateriaService)
        {
            _EspBateriaService = relEspBateriaService;
        }
        [HttpGet("{usuario_id}")]
       // [Authorize]
        public async Task<ActionResult<IEnumerable<BhBateriasModel>>> GetRelSolPractBhBateriaAsync(int usuario_id)
        {
            var atenciones = await _EspBateriaService.GetRelSolPractBhBateriaAsync(usuario_id);


            return Ok(atenciones);
        }

        [HttpGet("publicas")]
        public async Task<ActionResult<IEnumerable<BhBateriasPublicasModel>>> GetBateriasPublicasAsync()
        {
            var publicas = await _EspBateriaService.BateriaPublicamodel();
            return Ok(publicas);
        }
        [HttpPost]
        public async Task<IActionResult> InsertRelEspBaterias([FromBody] List<RelBateriasEspModel> model)
        {
            if (model == null || !model.Any())
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

                foreach (var modelo in model)
                {
                    await _EspBateriaService.InsertRelEspBaterias(modelo);
                }

                return Ok(new
                {
                    message = "Registros insertados exitosamente",
                    insertedRecords = model,
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

        [HttpDelete("{id}")]
        // [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> DeleteRelEspBateriasAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("id invalido");
            }
            var result = await _EspBateriaService.DeleteRelEspBateriasAsync(id);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }
    }
}
