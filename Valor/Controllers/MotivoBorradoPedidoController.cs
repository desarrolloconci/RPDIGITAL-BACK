using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotivoBorradoPedidoController : ControllerBase
    {
        private readonly IMotivoBorradoPedidoService _service;
        public MotivoBorradoPedidoController(IMotivoBorradoPedidoService service)
        {
            _service = service;
        }

        [HttpGet]
       // [Authorize]
        public async Task<IEnumerable<MotivoBorradoPedidoModel>> GetMotivoBorradoPedidoAsync()
        {
            return await _service.GetMotivoBorradoPedidoAsync();
        }
    }
}
