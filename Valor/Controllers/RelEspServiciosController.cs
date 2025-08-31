using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.RpInterfaces;
using ValoresData.Services.RpServices;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelEspServiciosController : ControllerBase
    {
        private readonly IRelEspeServiciosServices _relEspeServiciosServices;
        public RelEspServiciosController(IRelEspeServiciosServices relEspeServiciosServices)
        {
            _relEspeServiciosServices = relEspeServiciosServices;
        }
        [HttpGet("{usuario_id}")]
        //[Authorize(Roles = "Admin,Supervisor")]
        public async Task<ActionResult<IEnumerable<RelSolPractBhServicioSolModel>>> GetRelSolPractBhServicioSolAsync(int usuario_id)
        {
            var atenciones = await _relEspeServiciosServices.GetRelSolPractBhServicioSolAsync(usuario_id);
            

            return Ok(atenciones);
        }
    }
}
