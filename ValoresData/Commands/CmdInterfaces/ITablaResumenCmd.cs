using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface ITablaResumenCmd
    {
        public Task<IEnumerable<TablaResumenDto>> GetTablaResumenAsync(int idPrograma, int mes, int año);
    }
}
