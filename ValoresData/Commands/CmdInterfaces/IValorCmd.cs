using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;
using ValorModels.Models;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IValorCmd
    {
        public Task<IEnumerable<ValorModel>> GetValoresAsync();
        public Task<ValorModel> GetValorDetails(int id);
        public Task<IEnumerable<OsPlanDto>> GetOsAsync();
        public Task<IEnumerable<PlanDto>> GetPlaOs(int CodigoOS);
    }
}
