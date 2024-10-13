using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;
using ValorModels.Models;


namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValorController : ControllerBase
    {
        private readonly IValorService _valorService;

        public ValorController(IValorService service)
        {
            _valorService = service;
        }

        // GET: api/Valor
        [HttpGet]
        //[Authorize]
        public async Task<IEnumerable<ValorModel>> GetValores()
        {
            return await _valorService.GetValoresAsync();
        }
       
        // GET: api/Valor/5
        [HttpGet("{id}")]
       // [Authorize]
        public async Task<ActionResult<ValorModel>> GetValorModel(int id)
        {
            var valorModel = await _valorService.GetValorDetails(id);

            if (valorModel == null)
            {
                return NotFound("valor no encontrado");
            }

            return valorModel;
        }
     

        [HttpGet]
        [Route("/OS")]
       // [Authorize]
        public async Task<IEnumerable<OsPlanDto>> GetOsAsync()
        {
            return await _valorService.GetOsAsync();
        }

    }
}
