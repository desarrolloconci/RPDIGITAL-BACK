using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LimpiarGestionEstudioController : ControllerBase
    {
        private readonly ILimpiarGestionEstudioService _service;
        public LimpiarGestionEstudioController(ILimpiarGestionEstudioService service)
        {
            _service = service;
        }

        [HttpDelete]
        public async Task<IActionResult> LimpiarGestionAsync([FromBody] LimpiarGestionEstudioDto model)
        {
            if (model == null || string.IsNullOrEmpty(model.idPedido) || string.IsNullOrEmpty(model.idEstudio))
            {
                return BadRequest(new { message = "idPedido e idEstudio son obligatorios", success = false });
            }

            await _service.LimpiarGestionAsync(model.idPedido, model.idEstudio, model.usuario);

            return Ok(new { message = "Gestion del estudio limpiada correctamente", success = true });
        }
    }
}
