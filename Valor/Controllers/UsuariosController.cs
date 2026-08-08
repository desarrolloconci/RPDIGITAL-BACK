using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsusarioService _service;
        public UsuariosController(IUsusarioService service)
        {
            _service = service;
        }
        [HttpGet]
        //  [Authorize]
        public async Task<IEnumerable<UsuarioDto>> GetUsersAsync()
        {
            return await _service.GetUsersAsync();
        }

        // Listado de usuarios de GECLISA, para el picker de "vincular con Usuario_id de GECLISA"
        // al crear/editar un medico en Natanet.
        [HttpGet("Geclisa")]
        public async Task<IEnumerable<UsuarioGeclisaListadoDto>> GetUsuariosGeclisaAsync()
        {
            return await _service.GetUsuariosGeclisaAsync();
        }
    }
}
