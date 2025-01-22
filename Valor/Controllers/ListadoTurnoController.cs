using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;
using ValorModels.Dtos.BhDto;
using ValorModels.Models;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListadoTurnoController : ControllerBase
    {
        private readonly IListadoTurnoService _listadoturnoService;
        public ListadoTurnoController(IListadoTurnoService listadoTurnoService)
        {
            _listadoturnoService = listadoTurnoService;
        }


        [HttpGet]

        public async Task<IEnumerable<ListadoTurnosModel>> GetListadoTurnos()
        {

            return await _listadoturnoService.GetListadoTurnoAsync();
        }

        [HttpGet("{dni}/{fecha}")]

        public async Task<ActionResult<ListadoTurnosModel>> GetListadoTurnosByDni(string dni, DateOnly fecha)
        {
            var excepcion = await _listadoturnoService.GetListadoTurnoByDni(dni, fecha);

            if (excepcion == null)
            {
                return NotFound("Valor no encontrado");
            }

            return Ok(excepcion);

        }

        [HttpGet("Servicios/{dni}/{fecha}")]
        

        public async Task<ActionResult<ListadoServicioTurnoDto>> GetListadoserviciosByDni(string dni, DateOnly fecha)
        {
            var excepcion = await _listadoturnoService.GetListadoserviciosByDni(dni, fecha);

            if (excepcion == null)
            {
                return NotFound("Valor no encontrado");
            }

            return Ok(excepcion);

        }
    }
}
