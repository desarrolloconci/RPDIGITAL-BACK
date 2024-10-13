using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelSolPractBhUtilsController : ControllerBase
    {
        private readonly IRelSolPractBhUtilsService _solPractBhMetodoService;
        public RelSolPractBhUtilsController(IRelSolPractBhUtilsService relSolPractBhMetodoService)
        {
            _solPractBhMetodoService = relSolPractBhMetodoService;
        }

        [HttpGet]
        // [Authorize]
        public async Task<IEnumerable<RelSolPractBhMetodoModel>> GetRelSolPractBhMetodoAsync()
        {
            return await _solPractBhMetodoService.GetRelSolPractBhMetodoAsync();
        }

        [HttpGet]
        [Route("/unidad")]
        // [Authorize]
        public async Task<IEnumerable<RelSolPractBhUnidadModel>> GetRelSolPractBhUnidadAsync()
        {
            return await _solPractBhMetodoService.GetRelSolPractBhUnidadAsync();
        }
        [HttpGet]
        [Route("/servicio")]
        // [Authorize]
        public async Task<IEnumerable<RelSolPractBhServicioSolModel>> GetRelSolPractBhServicioSolAsync()
        {
            return await _solPractBhMetodoService.GetRelSolPractBhServicioSolAsync();
        }
        [HttpGet]
        [Route("/osBh")]
        // [Authorize]
        public async Task<IEnumerable<RelSolPractBhOsModel>> GetRelSolPractBhOsAsync()
        {
            return await _solPractBhMetodoService.GetRelSolPractBhOsAsync();
        }
        [HttpGet]
        [Route("/indBh")]
        // [Authorize]
        public async Task<IEnumerable<RelSolPractBhInductoresModel>> GetRelSolPractBhInductoresAsync()
        {
            return await _solPractBhMetodoService.GetRelSolPractBhInductoresAsync();
        }
        [HttpGet]
        [Route("/estProgBh")]
        // [Authorize]
        public async Task<IEnumerable<RelSolPractBhEstadoProgramaModel>> GetRelSolPractBhEstadoProgramaAsync()
        {
            return await _solPractBhMetodoService.GetRelSolPractBhEstadoProgramaAsync();
        }
        [HttpGet]
        [Route("/estTurnoBh")]
        // [Authorize]
        public async Task<IEnumerable<RelSolPractBhEstadoTurnoModel>> GetRelSolPractBhEstadoTurnoAsync()
        {
            return await _solPractBhMetodoService.GetRelSolPractBhEstadoTurnoAsync();
        }
    }
}
