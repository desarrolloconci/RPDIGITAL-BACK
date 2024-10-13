using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;
using ValorModels.Models;

namespace ValoresData.Services
{
    public class ValorService : IValorService
    {
        private readonly IValorCmd _valorCmd;
        public ValorService(IValorCmd valorCmd)
        {
            _valorCmd = valorCmd;
        }

        public Task<IEnumerable<OsPlanDto>> GetOsAsync()
        {
            return _valorCmd.GetOsAsync();
        }

        public Task<IEnumerable<PlanDto>> GetPlaOs(int planId)
        {
           return _valorCmd.GetPlaOs(planId);
        }

        public Task<ValorModel> GetValorDetails(int id)
        {
          return  _valorCmd.GetValorDetails(id);
        }

        public Task<IEnumerable<ValorModel>> GetValoresAsync()
        {
            return _valorCmd.GetValoresAsync();
        }

        Task<IEnumerable<OsPlanDto>> IValorService.GetOsAsync()
        {
            return _valorCmd.GetOsAsync();
        }
    }
}
