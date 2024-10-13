using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;

namespace ValoresData.Services
{
    public class TablaResumenService : ITablaResumenService
    {
        private readonly ITablaResumenCmd _ITablaResumenCmd;
        public TablaResumenService(ITablaResumenCmd tablaResumenCmd)
        {
            _ITablaResumenCmd = tablaResumenCmd;
        }
        public async Task<IEnumerable<TablaResumenDto>> GetTablaResumenAsync(int idPrograma, int mes, int año)
        {
            var result = await _ITablaResumenCmd.GetTablaResumenAsync(idPrograma, mes, año);

            return result;
        }
    }
}
