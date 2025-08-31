using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;
using ValorModels.Models;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    { private readonly ISendMailService _sendMailService;
        public MailController(ISendMailService sendMailService)
        {
            _sendMailService = sendMailService;
        }

        [HttpPost]
        //  [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> SendMailAsync([FromBody] SendEmailRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo inválido o ID no coincide");
            }


            _sendMailService.sendEmail(requestDto.Subject,requestDto.To,requestDto.Body);
           

            return Ok();

        }

    }
}
