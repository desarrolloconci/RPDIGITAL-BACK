using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Services.RpInterfaces
{
     public interface IRelEspeServiciosServices
    {
        public Task<IEnumerable<RelEspServiciosModel>> GetRelEspServiciosAsync();

        public Task<IEnumerable<RelSolPractBhServicioSolModel>> GetRelSolPractBhServicioSolAsync(int usuario_id);
    }
}
