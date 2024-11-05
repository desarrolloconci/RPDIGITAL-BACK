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

        public async Task<IEnumerable<SolPractBhDto>> GetSolPract(
                   DateTime? fechaCreacionRP,
    string? startFechaRP,
    string? endFechaRP,
    string? unidad,
    string? dni,
    string? metodo,
    string? prestador,
    string? estudio,
    string? estadoPractica,
    string? estadoTurno,
    string? usuario,
    string? servicio,
    string? obrasocial,
    string? ultimoContacto)
        {
            return await _solPractBhService.GetSolPractAsync(fechaCreacionRP, startFechaRP, endFechaRP, unidad, dni, metodo, prestador, estudio, estadoPractica, estadoTurno, usuario, servicio, obrasocial, ultimoContacto);
        }

        [HttpGet]
        [Route("/Ds")]
        public async Task<IEnumerable<SolPractBhDto>> GetSolPractDistinct(
                    DateTime? fechaCreacionRP,
    string? startFechaRP,
    string? endFechaRP,
    string? unidad,
    string? dni,
    string? metodo,
    string? prestador,
    string? estudio,
    string? estadoPractica,
    string? estadoTurno,
    string? usuario,
    string? servicio,
    string? obrasocial,
    string? ultimoContacto)
        {
            return await _solPractBhService.GetSolPractAsyncDistinct(fechaCreacionRP, startFechaRP, endFechaRP, unidad, dni, metodo, prestador, estudio, estadoPractica, estadoTurno, usuario, servicio, obrasocial, ultimoContacto);
        }

    }
}
