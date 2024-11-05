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
    public class RolUserController : ControllerBase
    {
        private readonly IRolUserService _RoluserService;
        public RolUserController(IRolUserService rolUserService)
        {
            _RoluserService = rolUserService;   
        }

        [HttpGet]
       // [Authorize]
        public async Task<IEnumerable<RolUserModel>> GetRolUserAsync()
        {
            return await _RoluserService.GetRolUserAsync();
        }
    }
}
