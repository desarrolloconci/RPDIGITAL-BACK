using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ValoresData.Services.LoginInterfaces;
using ValorModels.Dtos.LoginDto;


namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _loginService;
        public LoginController(ILogin login)
        {
            _loginService = login;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto userCredentials)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                LoginResponseDto data = await _loginService.RequestLoginAsync(userCredentials);
                return Ok(data);
            }
            catch (Exception ex)
            {
                // Loguea el error o devuelve un mensaje de error genérico
                return StatusCode(500, "Se produjo un error durante el inicio de sesión.");
            }
        }
    

        [HttpPost]
        [Route("register")]
       // [Authorize(Roles ="Admin")]
        public async Task<LoginResponseDto> RegisterUser([FromBody] UserDto userModel)
        {

            LoginResponseDto data = await _loginService.RequestCreateUserAsync(userModel);
            return data;
        }

        [HttpPost]
        [Route("register-medico")]
        //[Authorize(Roles = "Admin")]
        public async Task<LoginResponseDto> RegisterMedico([FromBody] RegisterMedicoDto medicoModel)
        {
            LoginResponseDto data = await _loginService.RequestCreateMedicoAsync(medicoModel);
            return data;
        }

        [HttpPut("{username}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(string username, [FromBody] UpdateUserDto userModel)
        {
            var result = await _loginService.RequestUpdateUserAsync(username, userModel);

            if (!result)
            {
                return NotFound("Usuario no encontrado");
            }

            return Ok();
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IEnumerable<UserResultDto>> GetUsersAsync()
        {
            return await _loginService.GetUsersAsync();
        }

        [HttpDelete("{username}")]
        //[Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> DeleteUserAsync(string username)
        {
            var result = await _loginService.DeleteUserAsync(username);

            if (!result)
            {
                return NotFound("Valor no encontrado o no pudo ser eliminado");
            }

            return Ok();
        }
    }
}
