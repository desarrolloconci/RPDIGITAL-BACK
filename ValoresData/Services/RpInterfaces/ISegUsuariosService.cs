using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.RpModels;

namespace ValoresData.Services.RpInterfaces
{
    public interface ISegUsuariosService
    {
        public Task<IEnumerable<SegUsuariosModel>> GetSegUsuariosAsync();
    }
}
