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
                     DateTime? fechaCreacionRP = null,
                     string unidad = null,
                     string dni = null,
                     string metodo = null,
                     string prestador = null,
                     string estudio = null,
                     string estadoPractica = null)
        {
            return await _solPractBhService.GetSolPractAsync(fechaCreacionRP, unidad, dni, metodo, prestador, estudio, estadoPractica);
        }

        [HttpGet]
        [Route("/Ds")]
        public async Task<IEnumerable<SolPractBhDto>> GetSolPractDistinct(
                   DateTime? fechaCreacionRP = null,
                   string unidad = null,
                   string dni = null,
                   string metodo = null,
                   string prestador = null,
                   string estudio = null,
                   string estadoPractica = null)
        {
            return await _solPractBhService.GetSolPractAsyncDistinct(fechaCreacionRP, unidad, dni, metodo, prestador, estudio, estadoPractica);
        }

    }
}
