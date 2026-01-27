using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.RpInterfaces;
using ValorModels.Models;
using ValorModels.Models.RpModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegGrupoGestionController : ControllerBase
    {
        private readonly ISegGrupoGestionService _service;
        public SegGrupoGestionController( ISegGrupoGestionService service   )
        {
            _service = service;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IEnumerable<SegGrupoGestionModel>> GetSegGrupoGestionAsync()
        {
            return await _service.GetSegGrupoGestionAsync();
        }
    }
}
