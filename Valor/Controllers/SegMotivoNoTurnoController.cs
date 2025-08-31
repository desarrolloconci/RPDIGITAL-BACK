using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.RpInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models;
using ValorModels.Models.RpModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegMotivoNoTurnoController : ControllerBase
    {
        private readonly ISegMotivoNoTurnoService _service;
        public SegMotivoNoTurnoController(ISegMotivoNoTurnoService service)
        {
            _service = service; 
        }
        [HttpGet]
        
        public async Task<IEnumerable<SegMotivoNoTurnoModel>> GetSegMotivoNoTurnoAsync()
        {
            return await _service.GetSegMotivoNoTurnoAsync();
        }
    }
}
