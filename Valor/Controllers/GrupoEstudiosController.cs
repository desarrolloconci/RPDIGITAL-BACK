using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Dtos;
using ValorModels.Models;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoEstudiosController : ControllerBase
    {
        private readonly IGrupoEstudiosService _grupoEstudiosService;
        public GrupoEstudiosController(IGrupoEstudiosService grupoEstudiosService)
        {
            _grupoEstudiosService = grupoEstudiosService;
        }

        [HttpGet]
        //  [Authorize]
        public async Task<IEnumerable<GrupoEstudiosModel>> GetGrupoEstudiosAsync()
        {
            return await _grupoEstudiosService.GetGrupoEstudiosAsync();
        }

        [HttpPost("crear-personal")]
       // [Authorize]
        public async Task<IActionResult> CrearBateriaPersonalAsync([FromBody] CrearBateriaPersonalDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre) || dto.EstudioIds == null || !dto.EstudioIds.Any())
            {
                return BadRequest(new { message = "El nombre y al menos un estudio son obligatorios", success = false });
            }

            try
            {
                var bateria = await _grupoEstudiosService.CrearBateriaPersonalAsync(dto);
                return Ok(new { message = "Batería creada exitosamente", bateria, success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, success = false });
            }
        }

        [HttpPut("{id}/editar-personal")]
      //  [Authorize]
        public async Task<IActionResult> EditarBateriaPersonalAsync(int id, [FromBody] CrearBateriaPersonalDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre) || dto.EstudioIds == null || !dto.EstudioIds.Any())
            {
                return BadRequest(new { message = "El nombre y al menos un estudio son obligatorios", success = false });
            }

            try
            {
                var resultado = await _grupoEstudiosService.EditarBateriaPersonalAsync(id, dto);
                return resultado switch
                {
                    ResultadoOperacionBateria.NoEncontrada => NotFound(new { message = "Batería no encontrada", success = false }),
                    ResultadoOperacionBateria.NoAutorizado => StatusCode(403, new { message = "No tenés permiso para modificar esta batería", success = false }),
                    _ => Ok(new { message = "Batería actualizada exitosamente", success = true }),
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, success = false });
            }
        }

        [HttpDelete("{id}/personal")]
       // [Authorize]
        public async Task<IActionResult> EliminarBateriaPersonalAsync(int id, [FromQuery] int usuarioId)
        {
            try
            {
                var resultado = await _grupoEstudiosService.EliminarBateriaPersonalAsync(id, usuarioId);
                return resultado switch
                {
                    ResultadoOperacionBateria.NoEncontrada => NotFound(new { message = "Batería no encontrada", success = false }),
                    ResultadoOperacionBateria.NoAutorizado => StatusCode(403, new { message = "No tenés permiso para eliminar esta batería", success = false }),
                    _ => Ok(new { message = "Batería eliminada exitosamente", success = true }),
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, success = false });
            }
        }

        [HttpPost("crear-general")]
        public async Task<IActionResult> CrearBateriaGeneralAsync([FromBody] CrearBateriaGeneralDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre) || dto.EstudioIds == null || !dto.EstudioIds.Any())
            {
                return BadRequest(new { message = "El nombre y al menos un estudio son obligatorios", success = false });
            }

            try
            {
                var bateria = await _grupoEstudiosService.CrearBateriaGeneralAsync(dto);
                return Ok(new { message = "Batería creada exitosamente", bateria, success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, success = false });
            }
        }

        [HttpPut("{id}/editar-general")]
        public async Task<IActionResult> EditarBateriaGeneralAsync(int id, [FromBody] CrearBateriaGeneralDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre) || dto.EstudioIds == null || !dto.EstudioIds.Any())
            {
                return BadRequest(new { message = "El nombre y al menos un estudio son obligatorios", success = false });
            }

            try
            {
                var resultado = await _grupoEstudiosService.EditarBateriaGeneralAsync(id, dto);
                return resultado switch
                {
                    ResultadoOperacionBateria.NoEncontrada => NotFound(new { message = "Batería no encontrada", success = false }),
                    _ => Ok(new { message = "Batería actualizada exitosamente", success = true }),
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, success = false });
            }
        }

        [HttpDelete("{id}/general")]
        public async Task<IActionResult> EliminarBateriaGeneralAsync(int id)
        {
            try
            {
                var resultado = await _grupoEstudiosService.EliminarBateriaGeneralAsync(id);
                return resultado switch
                {
                    ResultadoOperacionBateria.NoEncontrada => NotFound(new { message = "Batería no encontrada", success = false }),
                    _ => Ok(new { message = "Batería eliminada exitosamente", success = true }),
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, success = false });
            }
        }

        [HttpGet("{id}/medicos-asignados")]
        public async Task<ActionResult<IEnumerable<int>>> GetMedicosAsignadosAsync(int id)
        {
            return Ok(await _grupoEstudiosService.GetUsuariosAsignadosAsync(id));
        }

        [HttpGet("asignaciones")]
        public async Task<ActionResult<IEnumerable<AsignacionBateriaDto>>> GetTodasLasAsignacionesAsync()
        {
            return Ok(await _grupoEstudiosService.GetTodasLasAsignacionesAsync());
        }
    }
}
