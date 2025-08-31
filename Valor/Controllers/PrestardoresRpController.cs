using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.RpInterfaces;
using ValorModels.Models;
using ValorModels.Models.RpModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestardoresRpController : ControllerBase
    { private readonly IPrestadoresRpService _prestadoresRpService;
        public PrestardoresRpController(IPrestadoresRpService prestadoresRp)
        {
            _prestadoresRpService = prestadoresRp;
        }
        [HttpGet]
        //  [Authorize]
        public async Task<IEnumerable<PrestadoresRpModel>> GePrestadoresRpAsync()
        {
           return await _prestadoresRpService.GePrestadoresRpAsync();
        }
    }
}
