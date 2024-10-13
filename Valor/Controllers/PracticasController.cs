using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public class PracticasController : ControllerBase
    {
        private readonly IPracticasService _practicasService;
        public PracticasController(IPracticasService practicasService)
        {
            _practicasService = practicasService;
        }

        [HttpGet]
       // [Authorize]
        public async Task<IEnumerable<PracticasModel>> GetPracticasAsync()
        {
            return await _practicasService.GetPracticaAsync();
        }
        [HttpGet]
        [Route("/Dto")]
        //[Authorize]
        public async Task<IEnumerable<PracticasDto>> GetPracticasAsyncDTO()
        {
            return await _practicasService.GetPracticasDtoAsync();        
        }


        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult<PracticasModel>> GetPracticasAsyncById(int id)
        {
            var Programa = await _practicasService.GetPracticaAsyncById(id);

            if (Programa == null)
            {
                return NotFound("valor no encontrado");
            }

            return Programa;
        }
        [HttpDelete]
        //[Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult> DeletePracticaAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("id invalido");
            }
            var result = await _practicasService.DeletePracticaAsync(id);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }

        [HttpPost]
      //  [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> InsertPracticaAsync(PracticasModel practicas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo inválido o ID no coincide");
            }
            try
            {
                await _practicasService.InsertPracticaAsync(practicas);

                return CreatedAtAction("",new { message = "Registro Insertado", success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al insertar la práctica", success = false, error = ex.Message });
            }
    }
        [HttpPut]
      //  [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> UpdatePracticAsync(PracticasModel practicas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo Invalido");
            }
            try
            {
                bool updateSuccessful = await _practicasService.UpdatePracticaAsync(practicas);
                if (updateSuccessful)
                {
                    return Ok(new { message = "Registro Actualizado", practicas, success = true });

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
