using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionEstadoProgramaController : ControllerBase
    {
        private readonly IAsignacionEstadoProgramaService _asignacionEstadoProgramaService;
        public AsignacionEstadoProgramaController(IAsignacionEstadoProgramaService asignacionEstadoProgramaService)
        {
            _asignacionEstadoProgramaService = asignacionEstadoProgramaService;
        }
        [HttpGet]

        public async Task<IEnumerable<AsignacionEstadoProgramaModel>> GetAsignacionEstadoProgramaAsync()
        {

            return await _asignacionEstadoProgramaService.GetAsignacionEstadoProgramaAsync();
        }
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,Supervisor")]
        public async Task<ActionResult<AsignacionEstadoProgramaModel>> GetAsignacionEstadoProgramaDetailAsync(string dni, string unidad)
        {
            var asignacionModel = await _asignacionEstadoProgramaService.GetAsignacionEstadoProgramaDetailAsync(dni, unidad);   
            if (asignacionModel == null)
            {
                return NotFound("valor no encontrado");
            }

            return asignacionModel;
        }
        [HttpPost]
        // [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> InsertAsignacionProgramaAsync([FromBody] List<AsignacionEstadoProgramaModel> asignacion)
        {
            if (asignacion == null || !asignacion.Any())
            {
                return BadRequest(new { message = "El modelo está vacío o es inválido", success = false });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Uno o más modelos son inválidos",
                    success = false
                });
            }
            try
            {

                foreach (var model in asignacion)
                {
                    await _asignacionEstadoProgramaService.InsertAsignacionProgramaAsync(model);
                }

                return Ok(new
                {
                    message = "Registros insertados exitosamente",
                    insertedRecords = asignacion,
                    success = true
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new
                {
                    message = "Ocurrió un error al procesar la solicitud",
                    error = new
                    {
                        ex.Message,
                        ex.StackTrace,
                        InnerException = ex.InnerException?.Message
                    },
                    success = false
                });
            }
        }
        [HttpPut]
        // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> UpdatAsignacionEstadoProgramaAsync(AsignacionEstadoProgramaModel asignacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo Invalido");
            }

            try
            {
                bool updateSuccessful = await _asignacionEstadoProgramaService.UpdatAsignacionEstadoProgramaAsync(asignacion);

                if (updateSuccessful)
                {
                    return Ok(new { message = "Registro Actualizado", asignacion, success = true });

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
        public async Task<IActionResult> DeleteAsignacionEstadoProgramaAsync(string dni, string unidad)
        {

            var result = await _asignacionEstadoProgramaService.DeleteAsignacionEstadoProgramaAsync(dni, unidad);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }


    }
}

