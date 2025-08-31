using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.RpInterfaces;
using ValorModels.Dtos;
using ValorModels.Dtos.RpDto;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionesDiaController : ControllerBase
    {  private readonly IAtencionesDiaService _atencionesDiaService;
        public AtencionesDiaController(IAtencionesDiaService atencionesDia)
        {
            _atencionesDiaService=atencionesDia;
        }

        [HttpGet("{UserID}")]
        //[Authorize(Roles = "Admin,Supervisor")]
        public async Task<ActionResult<IEnumerable<AtencionesDiaModel>>> GetAtencionesDiaAsync(int UserID)
        {
            var atenciones = await _atencionesDiaService.GetAtencionesDiaAsync(UserID);
            if (atenciones == null)
            {
                return NotFound("valor no encontrado");
            }

            return Ok(atenciones);
        }
        [HttpGet("ServiciosMultiples")]
        //[Authorize(Roles = "Admin,Supervisor")]
        public async Task<ActionResult<IEnumerable<AtencionesDiaModel>>> GetAtencionesDiaServicioAsync(int UserID, string servicio)
        {
            var atenciones = await _atencionesDiaService.GetAtencionesDiaServicioAsync(UserID,servicio);
            if (atenciones == null)
            {
                return NotFound("valor no encontrado");
            }

            return Ok(atenciones);
        }

        [HttpGet("Servicios/{UserID}")]


        public async Task<ActionResult<IEnumerable<ListadoServicioAtencionDto>>> GetServicioAtencionesDiaAsync(int UserID)
        {
            var excepcion = await _atencionesDiaService.GetServicioAtencionesDiaAsync(UserID);

            if (excepcion == null)
            {
                return NotFound("Valor no encontrado");
            }

            return Ok(excepcion);

        }

    }
}
