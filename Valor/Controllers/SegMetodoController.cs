using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.RpInterfaces;
using ValorModels.Models;
using ValorModels.Models.RpModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegMetodoController : ControllerBase
    {
        private readonly ISegMetodoService _service;
        public SegMetodoController(ISegMetodoService service)
        {
            _service = service;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IEnumerable<SegMetodoModel>> GetMetodosAsync()
        {
            return await _service.GetMetodosAsync();
        }
    }
}
