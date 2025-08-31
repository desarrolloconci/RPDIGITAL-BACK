using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.RpInterfaces;
using ValorModels.Models.RpModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegUsuarioGestionController : ControllerBase
    {
        private readonly ISegUsarioGestionService _service;
        public SegUsuarioGestionController(ISegUsarioGestionService service)
        {
            _service = service;
        }

        [HttpPost]
        // [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> ManageSegUsuarioGestionAsync([FromBody] List<SegUsuarioGestionModel> model)
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
                bool cambios = false;

                foreach (var models in model)
                {
                    var resultado = await _service.ManageSegUsuarioGestionAsync(models);
                    if (resultado) cambios = true;
                }

                if (cambios)
                {
                    return Ok(new { message = "Cambios guardados en la base de datos", success = true });
                }
                else
                {
     
                    return Ok(new { message = "No se realizaron cambios en la base de datos", success = false });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error al procesar la solicitud",
                    error = ex.Message,
                    success = false
                });
            }
        }
    }
}
