using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;
using ValorModels.Models;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablaResumenController : ControllerBase
    {
        private readonly ITablaResumenService _tablaResumenService;
        public TablaResumenController(ITablaResumenService tablaResumenService)
        {
            _tablaResumenService = tablaResumenService;
        }
        [HttpGet ("{idPrograma}/{mes}/{año}")]
        // [Authorize]
        public async Task<IEnumerable<TablaResumenDto>> GetTablaResumenAsync(int idPrograma, int mes, int año)
        {
            return await _tablaResumenService.GetTablaResumenAsync(idPrograma, mes, año);
        }
    }
}
