using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RpBorradosController : ControllerBase
    {
        private readonly IRpBorradosService _service;
        public RpBorradosController(IRpBorradosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<RpBorradoModel>> GetRpBorradosAsync(string? dni = null, string? startFecha = null, string? endFecha = null, string? usuarioBorro = null, string? prestador = null)
        {
            return await _service.GetRpBorradosAsync(dni, startFecha, endFecha, usuarioBorro, prestador);
        }

        [HttpGet("usuarios")]
        public async Task<IEnumerable<string>> GetUsuariosBorradoAsync()
        {
            return await _service.GetUsuariosBorradoAsync();
        }

        [HttpGet("prestadores")]
        public async Task<IEnumerable<string>> GetPrestadoresBorradoAsync()
        {
            return await _service.GetPrestadoresBorradoAsync();
        }
    }
}
