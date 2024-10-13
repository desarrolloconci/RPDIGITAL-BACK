using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using ValoresData.Services;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;
using ValorModels.Models;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MontoMinimoProgramaController : ControllerBase
    {
        private readonly IMontoMinimoProgramaService _montoMinimoProgramaService;
        public MontoMinimoProgramaController(IMontoMinimoProgramaService montoMinimoProgramaService)
        {
            _montoMinimoProgramaService = montoMinimoProgramaService;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IEnumerable<MontoMinimoDto>> GetMontosAsync()
        {
            return await _montoMinimoProgramaService.GetMontoAsync();
        }

        [HttpGet("{id}/{mes}/{año}")]
        //[Authorize]
        public async Task<ActionResult<MontoMinimoProgramaModel>> GetMontoMesAñoAsyn(int id,int mes, int año)
        {
            var Monto = await _montoMinimoProgramaService.GetMontoMesAñoAsyn(id,mes,año);

            if (Monto == null)
            {
                return NotFound("valor no encontrado");
            }

            return Monto;
        }

   

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult<MontoMinimoProgramaModel>> GetMontoAsyncById(int id)
        {
            var Monto = await _montoMinimoProgramaService.GetMontoDetails(id);

            if (Monto == null)
            {
                return NotFound("valor no encontrado");
            }

            return Monto;
        }
        [HttpDelete]
        //[Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> DeleteMontoAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("id invalido");
            }
            var result = await _montoMinimoProgramaService.DeleteMontoAsync(id);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> InsertMontoAsync(MontoMinimoProgramaModel monto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo inválido o ID no coincide");
            }
            await _montoMinimoProgramaService.InsertMontoAsync(monto);

            return Ok(new { message = "Registro Insertado", success = true });
        }

        [HttpPut]
        //[Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> UpdateMontoAsync(MontoMinimoProgramaModel monto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo Invalido");
            }
            try
            {
                bool updateSuccessful = await _montoMinimoProgramaService.UpdateMontoAsync(monto);
                if (updateSuccessful)
                {
                    return Ok(new { message = "Registro Actualizado", monto, success = true });

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
