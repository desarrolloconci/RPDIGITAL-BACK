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
    public class MontoMinimoProgramaService : IMontoMinimoProgramaService
    {
        private readonly IMontoMinimoProgramaCmd _montoMinimo;
        public MontoMinimoProgramaService(IMontoMinimoProgramaCmd monto)
        {
            _montoMinimo = monto;
        }
        public async Task<bool> DeleteMontoAsync(int id)
        {
            return await _montoMinimo.DeleteMontoAsync(id);
        }
        public async Task<MontoMinimoProgramaModel> GetMontoMesAñoAsyn(int id, int mes, int año)
        {
            return await _montoMinimo.GetMontoMesAñoAsyn(id,mes,año);
        }
        public async Task<IEnumerable<MontoMinimoDto>> GetMontoAsync()
        {
           return await _montoMinimo.GetMontoAsync();
        }

        public async Task<MontoMinimoProgramaModel> GetMontoDetails(int id)
        {
            return await _montoMinimo.GetMontoDetailsByid(id);
        }

        public async Task<bool> InsertMontoAsync(MontoMinimoProgramaModel Monto)
        {
            return await _montoMinimo.InsertMontoAsync(Monto);
        }

        public async Task<bool> UpdateMontoAsync(MontoMinimoProgramaModel Monto)
        {
            return await _montoMinimo.UpdateMontoAsync(Monto);
        }
    }
}
