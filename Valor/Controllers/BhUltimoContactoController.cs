using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BhUltimoContactoController : ControllerBase
    {
        private readonly IBhUltimoContactoService _bhUltimoContacto;
        public BhUltimoContactoController(IBhUltimoContactoService bhUltimoContactoService)
        {
            _bhUltimoContacto = bhUltimoContactoService;
        }
        [HttpGet]
        public async Task<IEnumerable<BhUltimoContactoModel>> GetUltimoContactoAsync()
        {
            return await _bhUltimoContacto.GetUltimoContactoAsync();
        }

        [HttpPost]
        public async Task<IActionResult> InsertUltimoContactoAsync(BhUltimoContactoModel contacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo inválido o ID no coincide");
            }
            try
            {
                await _bhUltimoContacto.InsertUltimoContactoAsync(contacto);

                return Ok(new { message = "Registro Insertado", success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al insertar la práctica", success = false, error = ex.Message });
            }

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUltimoContactoBhAsync(int dni, string unidad)
        {

            var result = await _bhUltimoContacto.DeleteUltimoContactoBhAsync(dni, unidad);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUltimoContactoAsync(BhUltimoContactoModel contacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo Invalido");
            }
            try
            {
                bool updateSuccessful = await _bhUltimoContacto.UpdateUltimoContactoAsync(contacto);
                if (updateSuccessful)
                {
                    return Ok(new { message = "Registro Actualizado", contacto, success = true });

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
        [HttpGet("{dni}/{unidad}")]
        public async Task<ActionResult<BhUltimoContactoModel>> GetUltimoContactoADetailAsync(int dni, string unidad)
        {
            var obs = await _bhUltimoContacto.GetUltimoContactoADetailAsync(dni, unidad);
            if (obs == null)
            {
                return NotFound("valor no encontrado ULTIMO CONTACTO ");
            }
            return obs;
        }
    }
}
