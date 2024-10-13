using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;
using ValorModels.Models;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IValorService
    {
        public Task<IEnumerable<ValorModel>> GetValoresAsync();
        public Task<ValorModel> GetValorDetails(int id);
        public Task<IEnumerable<OsPlanDto>> GetOsAsync();
        public Task<IEnumerable<PlanDto>> GetPlaOs(int planId);
    }
}
