using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using ValoresData.Services.RpInterfaces;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegUsuariosController : ControllerBase
    {
        private readonly ISegUsuariosService _service;
        public SegUsuariosController(ISegUsuariosService service)
        {
            _service = service;
        }
        [HttpGet]
        // [Authorize]
        public async Task<IEnumerable<SegUsuariosModel>> GetSegUsuariosAsync()
        {
            return await _service.GetSegUsuariosAsync();
        }

    }
}
