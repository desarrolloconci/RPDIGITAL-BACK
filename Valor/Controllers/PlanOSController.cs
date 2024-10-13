using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanOSController : ControllerBase
    {
        private readonly IValorService _valorService;

        public PlanOSController(IValorService service)
        {
            _valorService = service;
        }

        [HttpGet("{id}")]
       // [Authorize]    
        public async Task<IActionResult> GetPlan(int id)
        {
            var planOs = await _valorService.GetPlaOs(id);

            if (planOs == null)
            {
                return BadRequest();
            }

            return Ok(planOs);
        }
    }
}
