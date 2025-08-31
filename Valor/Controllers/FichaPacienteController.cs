using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.RpInterfaces;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichaPacienteController : ControllerBase
    {
        private readonly IFichaPacienteServicio _ficha;
        public FichaPacienteController(IFichaPacienteServicio fichaPacienteServicio)
        {
            _ficha = fichaPacienteServicio;
        }

        [HttpGet]

        public async Task<IEnumerable<FichaPacienteModel>> GetFichaPacienteAsync()
        {

            return await _ficha.GetFichaPacienteAsync();
        }
        [HttpGet("{dni}")]
        public async Task<ActionResult<FichaPacienteModel>> GetFichaPacienteDetailAsync(int dni)
        {
            var pac = await _ficha.GetFichaPacienteDetailAsync(dni);
            if (pac == null)
            {
                return NotFound("Paciente no encontrado ");
            }
            return pac;
        }
    }
}
