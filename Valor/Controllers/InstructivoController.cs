using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructivoController : ControllerBase
    { private readonly IInstructivoService _instructivoService;
        public InstructivoController(IInstructivoService instructivoService)
        {
            _instructivoService = instructivoService;
        }


        [HttpGet]
        //[Authorize]
        public async Task<IEnumerable<InstructivoModel>> GetInstructivosAsync()
        {
            return await _instructivoService.GetInstructivoAsync();
        }
        [HttpGet("{id}")]
       //[Authorize]
        public async Task<ActionResult<InstructivoModel>> GetInstructivosByCodNomAsync(int id)
        {
            var practica = await _instructivoService.GetInstructivoByCodnomAsync(id);
            if (practica == null)
            {
                return NotFound("Valor no encontrado");
            }
            return Ok(practica);
        }
    }

}

