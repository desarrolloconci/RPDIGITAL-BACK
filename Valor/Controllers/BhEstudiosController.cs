using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BhEstudiosController : ControllerBase
    { private readonly IBhEstudiosService _bhEstudiosService;
        public BhEstudiosController(IBhEstudiosService bhEstudiosService)
        {
            _bhEstudiosService = bhEstudiosService;
        }
        [HttpGet]
       // [Authorize]
        public async Task<IEnumerable<BhEstudiosModel>> GetBhEstudiosAsync([FromQuery] string? search)
        {
            return await _bhEstudiosService.GetBhEstudiosAsync(search);
        }
    }
}
