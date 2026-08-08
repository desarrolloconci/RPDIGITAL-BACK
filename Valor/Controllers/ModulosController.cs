using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos.ModulosDto;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulosController : ControllerBase
    {
        private readonly IModulosService _modulosService;

        public ModulosController(IModulosService modulosService)
        {
            _modulosService = modulosService;
        }

        // Epic 6: detalle de modulo/prestacion (click sobre un item puntual). Responsabilidad
        // del frontend: 1 click = 1 sola llamada (ver traza original, Fase 10).
        [HttpGet("{meId}/Detalle")]
        public async Task<ActionResult<IEnumerable<ModuloDetalleDto>>> GetDetalle(int meId)
        {
            var result = await _modulosService.GetDetalleModuloAsync(meId);

            return Ok(result);
        }
    }
}
