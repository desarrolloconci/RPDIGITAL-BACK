using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Dtos.BhDto;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolPractBhController : ControllerBase
    {
        private readonly ISolPractBhService _solPractBhService;
        public SolPractBhController(ISolPractBhService solPractBhService)
        {
            _solPractBhService = solPractBhService;
        }

        [HttpGet]

        public async Task<IEnumerable<SolPractBhMetodoDto>> GetSolPract(
            DateTime? fechaCreacionRP,
            string? startFechaRP,
            string? endFechaRP,
            [FromQuery] List<string>? unidad,
            string? dni,
            string? metodo,
            string? prestador,
            string? estudio,
            int? estadoPrograma,
            [FromQuery] List<int>? estadoTurno,
            string? usuario,
            string? servicio,
            string? obrasocial,
            string? ultimoContacto,
            int? inductor)
        {
            return await _solPractBhService.GetSolPractAsync(fechaCreacionRP, startFechaRP, endFechaRP, unidad, dni, metodo, prestador, estudio, estadoPrograma, estadoTurno, usuario, servicio, obrasocial, ultimoContacto, inductor);
        }


        [HttpGet]
        [Route("/Ds")]
        public async Task<IEnumerable<SolPractBhDto>> GetSolPractDistinct(
            DateTime? fechaCreacionRP,
            string? startFechaRP,
            string? endFechaRP,
            [FromQuery(Name = "unidad[]")] List<string>? unidad,
            string? dni,
            string? metodo,
            string? prestador,
            string? estudio,
            int? estadoPrograma,
            [FromQuery(Name = "estadoTurno[]")] List<int>? estadoTurno,
            string? usuario,
            string? servicio,
            string? obrasocial,
            string? ultimoContacto,
            int? inductor)
        {
            return await _solPractBhService.GetSolPractAsyncDistinct(fechaCreacionRP, startFechaRP, endFechaRP, unidad, dni, metodo, prestador, estudio, estadoPrograma, estadoTurno, usuario, servicio, obrasocial, ultimoContacto, inductor);
        }
        [HttpGet]
        [Route("/Rp")]
        public async Task<IEnumerable<SolPractBhDto>> GetSolPractRpAsync(
          DateTime? fechaCreacionRP,
          string? startFechaRP,
          string? endFechaRP,
          [FromQuery(Name = "unidad[]")] List<string>? unidad,
          string? dni,
          string? metodo,
          string? prestador,
          string? estudio,
          int? estadoPrograma,
          [FromQuery(Name = "estadoTurno[]")] List<string>? estadoTurno,
        //  string? estadoTurno,
          string? usuario,
          string? servicio,
          string? obrasocial,
          string? ultimoContacto,
          int? inductor,
          int? grupoGestionId)
        {
            return await _solPractBhService.GetSolPractRpAsync(startFechaRP, endFechaRP, unidad, dni, metodo, prestador, estudio, estadoPrograma, estadoTurno, usuario, servicio, obrasocial, ultimoContacto, inductor, grupoGestionId);
        }

        [HttpGet]
        [Route("/RpPdf")]
        public async Task<IEnumerable<SolPractBhRpDto>> GetSolPractRpPdfAsync(string IDPEDIDO, string metodo)
        {
            return await _solPractBhService.GetSolPractRpPdfAsync(IDPEDIDO, metodo);
        }
        [HttpPut]
        // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> UpdateSolPractRpAsync(SolPractBhPedidoManualModel SolPract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo Invalido");
            }

            try
            {
                bool updateSuccessful = await _solPractBhService.UpdateSolPractRpAsync(SolPract);

                if (updateSuccessful)
                {
                    return Ok(new { message = "Registro Actualizado", SolPract, success = true });

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
        [HttpGet]
        [Route("/Rpseguimiento")]
        public async Task<IEnumerable<SolPractBhMetodoDto>> GetSolPractSeguimientoAsync(
            DateTime? fechaCreacionRP,
            string? startFechaRP,
            string? endFechaRP,
            [FromQuery] List<string>? unidad,
            string? dni,
            string? metodo,
            string? prestador,
            string? estudio,
            int? estadoPrograma,
            [FromQuery] List<int>? estadoTurno,
            string? usuario,
            string? servicio,
            string? obrasocial,
            string? ultimoContacto,
            int? inductor)
        {
            return await _solPractBhService.GetSolPractSeguimientoAsync(fechaCreacionRP, startFechaRP, endFechaRP, unidad, dni, metodo, prestador, estudio, estadoPrograma, estadoTurno, usuario, servicio, obrasocial, ultimoContacto, inductor);
        }
        [HttpGet]
        [Route("/PedidosAnteriores")]
        public async Task<IEnumerable<SolPractBhDto>> GetPedidosAnterioresPorDniAsync(string dni)
        {
            return await _solPractBhService.GetPedidosAnterioresPorDniAsync(dni);
        }

        [HttpPost]
        [Route("/Dni/Existe")]
        public async Task<ActionResult<Dictionary<string, bool>>> GetDnisConRpAsync([FromBody] List<string> dnis)
        {
            if (dnis == null || !dnis.Any())
            {
                return BadRequest(new { message = "La lista de dni está vacía o es inválida", success = false });
            }

            var result = await _solPractBhService.GetDnisConRpAsync(dnis);

            return Ok(result);
        }

        [HttpDelete]
        // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> DeleteSolPractTotalAsync(string idpedido)
        {

            var result = await _solPractBhService.DeleteSolPractTotalAsync(idpedido);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return NoContent();
        }
    }
}
