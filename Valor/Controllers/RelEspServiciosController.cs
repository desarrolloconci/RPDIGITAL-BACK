using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.RpInterfaces;
using ValoresData.Services.RpServices;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelEspServiciosController : ControllerBase
    {
        private readonly IRelEspeServiciosServices _relEspeServiciosServices;
        public RelEspServiciosController(IRelEspeServiciosServices relEspeServiciosServices)
        {
            _relEspeServiciosServices = relEspeServiciosServices;
        }
        [HttpGet("{usuario_id}")]
        //[Authorize(Roles = "Admin,Supervisor")]
        public async Task<ActionResult<IEnumerable<RelSolPractBhServicioSolModel>>> GetRelSolPractBhServicioSolAsync(int usuario_id)
        {
            var atenciones = await _relEspeServiciosServices.GetRelSolPractBhServicioSolAsync(usuario_id);


            return Ok(atenciones);
        }

        [HttpGet("detalle/{usuario_id}")]
        public async Task<ActionResult<IEnumerable<RelEspServiciosModel>>> GetRelEspServiciosDetailsAsync(int usuario_id)
        {
            var servicios = await _relEspeServiciosServices.GetRelEspServiciosDetailsAsync(usuario_id);
            return Ok(servicios);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRelEspServicios([FromBody] List<RelEspServicioModel> model)
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
                    await _relEspeServiciosServices.InsertRelEspServicios(modelo);
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
        public async Task<IActionResult> DeleteRelEspServiciosAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("id invalido");
            }
            var result = await _relEspeServiciosServices.DeleteRelEspServiciosAsync(id);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }
    }
}
