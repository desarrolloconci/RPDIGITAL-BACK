using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhInterfaces
{
    public interface IGrupoEstudiosService
    {
        public Task<IEnumerable<GrupoEstudiosModel>> GetGrupoEstudiosAsync();
    }

}
