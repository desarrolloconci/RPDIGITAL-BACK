using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IValorResutlService
    {
        public Task<IEnumerable<ValoresResultDto>> GetValorResultAsync(int idPrograma, int codigoOS, int planId);
    }
}
