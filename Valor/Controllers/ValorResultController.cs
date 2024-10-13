using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValoresData.Services;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValorResultController : ControllerBase
    {
        private readonly IValorResutlService _valorResutlService;
        public ValorResultController(IValorResutlService valorResutlService)
        {
            _valorResutlService = valorResutlService;
        }

        [HttpGet("{idPrograma}/{codigoOS}/{planId}")]
        //[Authorize]
        public async Task<IEnumerable<ValoresResultDto>> GetValorResult(int idPrograma, int codigoOS, int planId)
        {
            var valorresult = await _valorResutlService.GetValorResultAsync(idPrograma, codigoOS, planId);

            if (valorresult == null)
            {
                return Enumerable.Empty<ValoresResultDto>();
            }

            return  valorresult;

        }
    }
}
