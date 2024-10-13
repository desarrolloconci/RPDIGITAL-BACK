using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IAuxPracticasCmd
    {
        public Task<IEnumerable<AuxPracticasModel>> GetAuxPracticasAsync();
        public Task<AuxPracticasModel> GetAuxPracticasByCodNomAsync(string codnom);
    }
}
