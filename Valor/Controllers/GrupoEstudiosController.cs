using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.SolPractBhInterfaces;
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
    }
}
