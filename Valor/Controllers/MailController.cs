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
    { private readonly IMailQueue _mailQueue;
        public MailController(IMailQueue mailQueue)
        {
            _mailQueue = mailQueue;
        }

        [HttpPost]
        //  [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> SendMailAsync([FromBody] SendEmailRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo inválido o ID no coincide");
            }


            await _mailQueue.QueueEmailAsync(requestDto.Subject, requestDto.To, requestDto.Body);


            return Ok();

        }

    }
}
