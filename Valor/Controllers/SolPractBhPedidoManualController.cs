using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.SolPractBhServices;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolPractBhPedidoManualController : ControllerBase
    {
        private readonly ISolPractBhPedidoManualService _solPractBhPedidoManualService;
        public SolPractBhPedidoManualController(ISolPractBhPedidoManualService solPractBhPedidoManualService)
        {
            _solPractBhPedidoManualService = solPractBhPedidoManualService;
        }

        [HttpGet]

        public async Task<IEnumerable<SolPractBhPedidoManualModel>> GetSolPractBhPedidoManualAsync()
        {

            return await _solPractBhPedidoManualService.GetSolPractBhPedidoManualAsync();
        }
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,Supervisor")]
        public async Task<ActionResult<SolPractBhPedidoManualModel>> GetSolPractBhPedidoManuallAsyncById(int id)
        {
            var SolPractModel = await _solPractBhPedidoManualService.GetSolPractBhPedidoManuallAsyncById(id);

            if (SolPractModel == null)
            {
                return NotFound("valor no encontrado");
            }

            return SolPractModel;
        }
        [HttpPost]
        // [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> InsertSolPractBhPedidoManualAsync([FromBody] List<SolPractBhPedidoManualModel> SolPractBhPedidoManualModel)
        {
            if (SolPractBhPedidoManualModel == null)
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
                
                SolPractBhPedidoManualModel = SolPractBhPedidoManualModel
                    .GroupBy(m => new { m.IDESTUDIO, m.DIAGNÓSTICO, m.METODOPRACTICA })
                    .Select(g => g.First())
                    .ToList();

                string idPedido = Guid.NewGuid().ToString();

                foreach (var model in SolPractBhPedidoManualModel)
                {
                    model.IDPEDIDO = idPedido;
                }

                foreach (var model in SolPractBhPedidoManualModel)
                {
                    model.IDPEDIDO = idPedido;
                }

                foreach (var model in SolPractBhPedidoManualModel)
                {
                    await _solPractBhPedidoManualService.InsertSolPractBhPedidoManualAsync(model);
                }

                return Ok(new
                {
                    message = "Registro insertado exitosamente",
                    insertedRecords = SolPractBhPedidoManualModel,
                    success = true
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new
                {
                    message = ex.Message,
                    success = false
                });
            }
        }
        [HttpPut]
        // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> UpdateSolPractBhPedidoManualAsync(SolPractBhPedidoManualModel relSolPractModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo Invalido");
            }

            try
            {
                bool updateSuccessful = await _solPractBhPedidoManualService.UpdateSolPractBhPedidoManualAsync(relSolPractModel);

                if (updateSuccessful)
                {
                    return Ok(new { message = "Registro Actualizado", relSolPractModel, success = true });

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
        public async Task<IActionResult> DeletRelSolPractBhPedidoManualoAsync(int id)
        {
            {

                var result = await _solPractBhPedidoManualService.DeletRelSolPractBhPedidoManualoAsync(id);

                if (!result)
                {
                    return NotFound("Valor no encontrado o no pudo ser eliminado");
                }

                return NoContent();
            }

        }
    }
}
