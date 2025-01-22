using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Asignacion_inductorController : ControllerBase
    {
        private readonly IAsignacionInductoresService _asignacionInductores;
        public Asignacion_inductorController(IAsignacionInductoresService asignacion)
        {
            _asignacionInductores = asignacion;
        }
        [HttpGet]

        public async Task<IEnumerable<AsignacionInductoresModel>> GetAsignacionInductoresAsync()
        {

            return await _asignacionInductores.GetAsignacionInductoresAsync();
        }
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,Supervisor")]
        public async Task<ActionResult<AsignacionInductoresModel>> GetAsignacionInductoresADetailAsync(string dni, string unidad)
        {
            var asignacionModel = await _asignacionInductores.GetAsignacionInductoresADetailAsync(dni, unidad);
            if (asignacionModel == null)
            {
                return NotFound("valor no encontrado");
            }

            return asignacionModel;
        }
        [HttpPost]
        // [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> InsertAsignacionInductoresAsync([FromBody] List<AsignacionInductoresModel> asignacion)
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
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)),
                    success = false
                });
            }
            try
            {

                foreach (var model in asignacion)
                {
                    await _asignacionInductores.InsertAsignacionInductoresAsync(model);
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
                    error = ex,
                    success = false
                });
            }
        }
        [HttpPut]
        // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> UpdatAsignacionInductoresAsync(AsignacionInductoresModel asignacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo Invalido");
            }

            try
            {
                bool updateSuccessful = await _asignacionInductores.UpdatAsignacionInductoresAsync(asignacion);

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
        public async Task<IActionResult> DeleteAsignacionInductoresAsync(string dni, string unidad)
        {

            var result = await _asignacionInductores.DeleteAsignacionInductoresAsync(dni,unidad);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }
       

    }
}

