using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using ValoresData.Services;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuxPracticasController : ControllerBase
    {
        private readonly IAuxPracticasService _auxPracticasService;
        public AuxPracticasController(IAuxPracticasService auxPracticasService)
        {
            _auxPracticasService = auxPracticasService;
        }

        [HttpGet]
       // [Authorize]
        public async Task<IEnumerable<AuxPracticasModel>> GetAuxPracticasAsync()
        {
            return await _auxPracticasService.GetAuxPracticasAsync();
        }
        [HttpGet("{codnom}")]
      //  [Authorize]
        public async Task<ActionResult<AuxPracticasModel>> GetAuxPracticasByCodNomAsync(string codnom)
        {
            var practica = await _auxPracticasService.GetAuxPracticasByCodNomAsync(codnom);
            if (practica == null)
            {
                return NotFound("Valor no encontrado");
            }
            return Ok(practica);
        }
    }    
}
