using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models;

namespace ValoresData.Services
{
    public class ExcepcionOsPlanCodService : IExcepcionOsPlanCodService
    {
        private readonly IExcepcionOsPlanCodCmd _excepcion;
        public ExcepcionOsPlanCodService(IExcepcionOsPlanCodCmd excepcionOsPlanCodCmd)
        {
            _excepcion = excepcionOsPlanCodCmd;
        }
        public async Task<bool> DeleteExcepcionAsync(int id)
        {
          return await  _excepcion.DeleteExcepcionAsync(id);
        }

        public async Task<ExcepcionOsPlanCodModel> GetExcepcionesDetailsOSPlanPracAsync(int os, int plan, string cod)
        {
            return await _excepcion.GetExcepcionesDetailsOSPlanPracAsync(os,plan, cod);
        }
        public async Task<IEnumerable<ExcepcionOsPlanCodModel>> GetExcepcionesAsync()
        {
           return await _excepcion.GetExcepcionesAsync();
        }

        public async Task<ExcepcionOsPlanCodModel> GetExcepcionesDetailsAsync(int id)
        {
            return await _excepcion.GetExcepcionesDetailsAsync(id);
        }

        public async Task<ExcepcionOsPlanCodModel> GetExcepcionesDetailsByOsAsync(int os)
        {
            return await _excepcion.GetExcepcionesDetailsByOsAsync(os);
        }

        public async Task<bool> InsertExcepcionAsync(ExcepcionOsPlanCodModel excepciones)
        {
            return await _excepcion.InsertExcepcionAsync(excepciones);
        }

        public async Task<bool> UpdateExcepcionAsync(ExcepcionOsPlanCodModel excepciones)
        {
            return await _excepcion.UpdateExcepcionAsync(excepciones);
        }
    }
}
