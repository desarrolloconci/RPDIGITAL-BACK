using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models.RpModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketSoporteController : ControllerBase
    {
        private readonly ITicketSoporteService _service;
        public TicketSoporteController(ITicketSoporteService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<TicketSoporteModel>> InsertTicketSoporteAsync([FromBody] TicketSoporteModel model)
        {
            var result = await _service.InsertTicketSoporteAsync(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IEnumerable<TicketSoporteModel>> GetTicketsSoporteAsync(string? usuario = null, string? ticketId = null, string? startFecha = null, string? endFecha = null)
        {
            return await _service.GetTicketsSoporteAsync(usuario, ticketId, startFecha, endFecha);
        }
    }
}
