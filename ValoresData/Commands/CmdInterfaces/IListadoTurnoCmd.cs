using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IListadoTurnoCmd
    {
        public Task<IEnumerable<ListadoTurnosModel>> GetListadoTurnoAsync();

        public Task<IEnumerable<ListadoTurnosModel>> GetListadoTurnoByDni(string dni, DateOnly fecha);
    }
}
