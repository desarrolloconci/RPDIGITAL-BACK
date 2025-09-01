using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.RpModels;

namespace ValoresData.Services.RpInterfaces
{
   public interface ISegGrupoGestionService
    {
        public Task<IEnumerable<SegGrupoGestionModel>> GetSegGrupoGestionAsync();
    }
}
