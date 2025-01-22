using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoPedidoManualController : ControllerBase
    { private readonly IEstadoPedidoManualService _estadoPedidoManualService;
        public EstadoPedidoManualController(IEstadoPedidoManualService estadoPedidoManualService)
        {
            _estadoPedidoManualService = estadoPedidoManualService;
        }

        [HttpGet]
        public async Task<IEnumerable<EstadoPedidoManualModel>> GetEstadoPedidoManualAsync()
        {
            return await _estadoPedidoManualService.GetEstadoPedidoManualAsync();

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoPedidoManualModel>> GetEstadoPedidoManualById(int id)
        {
            var EstadoPedidoManual = await _estadoPedidoManualService.GetEstadoPedidoManualById(id);
            if (EstadoPedidoManual == null)
            {
                return NotFound("valor no encontrado");
            }
            return EstadoPedidoManual;
        }
        [HttpPost]
        public async Task<IActionResult> InsertEstadoPedidoManualAsync(EstadoPedidoManualModel estadoPedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo inválido o ID no coincide");
            }
            try
            {
                await _estadoPedidoManualService.InsertEstadoPedidoManualAsync(estadoPedido);

                return Ok(new { message = "Registro Insertado", success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al insertar la práctica", success = false, error = ex.Message });
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEstadoPedidoManualAsync(EstadoPedidoManualModel estadoPedido)
        {
            if (estadoPedido.id <= 0)
            {
                return BadRequest("id invalido");
            }
            var result = await _estadoPedidoManualService.DeleteEstadoPedidoManualAsyncVarios(estadoPedido);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEstadoPedidoManualAsync(EstadoPedidoManualModel estadoPedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo Invalido");
            }
            try
            {
                bool updateSuccessful = await _estadoPedidoManualService.UpdateEstadoPedidoManualAsync(estadoPedido);
                if (updateSuccessful)
                {
                    return Ok(new { message = "Registro Actualizado", estadoPedido, success = true });

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
