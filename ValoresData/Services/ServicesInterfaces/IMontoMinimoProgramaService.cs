using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;
using ValorModels.Models;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IMontoMinimoProgramaService
    {
        public Task<IEnumerable<MontoMinimoDto>> GetMontoAsync();
        public Task<MontoMinimoProgramaModel> GetMontoMesAñoAsyn(int id,int mes, int año);
        public Task<MontoMinimoProgramaModel> GetMontoDetails(int id);
        public Task<bool> InsertMontoAsync(MontoMinimoProgramaModel Monto);
        public Task<bool> UpdateMontoAsync(MontoMinimoProgramaModel Monto);
        public Task<bool> DeleteMontoAsync(int id);
    }
}
