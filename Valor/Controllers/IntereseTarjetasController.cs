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
    public class IntereseTarjetasController : ControllerBase
    {
        private readonly IInteresTarjetasServices _interesTarjetasServices;
        public IntereseTarjetasController(IInteresTarjetasServices interesTarjetas)
        {
            _interesTarjetasServices=interesTarjetas;

        }

        [HttpGet]
       // [Authorize]
        public async Task<IEnumerable<InteresesTarjetasModel>> GetInteres()
        {
            return await _interesTarjetasServices.GetInteresesAsync();
        }

     /*   [HttpPost]
       // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<bool> InsertPrograma(InteresesTarjetasModel intereses)
        {
            await _interesTarjetasServices.InsertInteresAsync(intereses);

            if (intereses != null)
            {
                return true;
            }
            return false;
        }
        [HttpGet("{id}")]
      //  [Authorize(Roles = "Admin,Supervisor")]
        public async Task<ActionResult<InteresesTarjetasModel>> GetInteresDetailsAsync(int id)
        {
            var ProgramasModel = await _interesTarjetasServices.GetInteresAsyncById(id);

            if (ProgramasModel == null)
            {
                return NotFound("valor no encontrado");
            }

            return ProgramasModel;
        }

        [HttpPut]
       // [Authorize(Roles = "Admin,Supervisor")]
        public async Task<bool> UpdateInteresAsync(InteresesTarjetasModel intereses)
        {
            if (intereses != null)
            {
                await _interesTarjetasServices.UpdateInteresAsync(intereses);
                return true;
            }
            return false;

        }
        [HttpDelete]
     //   [Authorize(Roles = "Admin,Supervisor")]
        public async Task<bool> DeleteInteres(int id)
        {
            var Programa = await _interesTarjetasServices.DeleteInteresAsync(id);

            if (Programa == false)
            {
                return false;
            }

            return true;
        }*/

    }
}
