using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models.BhModels;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BhBateriasEstudiosController : ControllerBase
    {
        private readonly IBhBateriasEstudiosService _bh;
        public BhBateriasEstudiosController(IBhBateriasEstudiosService bh)
        {
            _bh = bh;
        }
        [HttpGet]

        public async Task<IEnumerable<BhBateriasModel>> GeBhBateriasEstudiosAsync()
        {

            return await _bh.GeBhBateriasEstudiosAsync();
        }
        [HttpGet("{GrupoId}")]
       // [Authorize]
        public async Task<IEnumerable<BhBateriasEstudiosModel>> GetBhBateriasEstudiosGrupoAsync(int GrupoId)
        {
            var asignacionModel = await _bh.GetBhBateriasEstudiosGrupoAsync(GrupoId);
            if (asignacionModel == null)
            {
                return null;
            }

            return asignacionModel;
        }
    }
}
