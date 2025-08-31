using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.RpInterfaces;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NnRelMotivoNotunoController : ControllerBase
    {
        private readonly INnRelMotivoNoTurnoService _nnRel;
        public NnRelMotivoNotunoController(INnRelMotivoNoTurnoService nnRelMotivoNo)
        {
            _nnRel = nnRelMotivoNo;
        }
        [HttpGet]
        public async Task<IEnumerable<NnRelMotivoNoTurnoModel>> GetRelMotivoNoTurnoAsync()
        {
            return await _nnRel.GetRelMotivoNoTurnoAsync();
        }

        [HttpPost]
        // [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> InsertMotivoNoTurnoAsync([FromBody] List<NnRelMotivoNoTurnoModel> motivoNoTurnoModel)
        {
            if (motivoNoTurnoModel == null || !motivoNoTurnoModel.Any())
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

                foreach (var model in motivoNoTurnoModel)
                {
                    await _nnRel.InsertMotivoNoTurnoAsync(model);
                }

                return Ok(new
                {
                    message = "Registros insertados exitosamente",
                    insertedRecords = motivoNoTurnoModel,
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
        public async Task<IActionResult> UpdateMotivoNoTurnoAsync(NnRelMotivoNoTurnoModel motivoNoTurnoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo Invalido");
            }

            try
            {
                bool updateSuccessful = await _nnRel.UpdateMotivoNoTurnoAsync(motivoNoTurnoModel);

                if (updateSuccessful)
                {
                    return Ok(new { message = "Registro Actualizado", motivoNoTurnoModel, success = true });

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
        public async Task<IActionResult> DeletMotivoNoTurnoAsync(string idpedido, string metodo)
        {

            var result = await _nnRel.DeletMotivoNoTurnoAsync(idpedido, metodo);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("/Unit")]
        // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> DeletMotivoNoTurnoUnitarioAsync(string idEstudio, string idPedido)
        {

            var result = await _nnRel.DeletMotivoNoTurnoUnitarioAsync(idEstudio, idPedido);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }
    }
}
