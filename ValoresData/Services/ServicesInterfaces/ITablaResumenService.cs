using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface ITablaResumenService
    {
        public Task<IEnumerable<TablaResumenDto>> GetTablaResumenAsync( int idPrograma, int mes, int año);
    }
}
