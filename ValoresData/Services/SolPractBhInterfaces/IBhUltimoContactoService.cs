using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhInterfaces
{
    public interface IBhUltimoContactoService
    {
        public Task<IEnumerable<BhUltimoContactoModel>> GetUltimoContactoAsync();
        public Task<BhUltimoContactoModel> GetUltimoContactoADetailAsync(int dni, string unidad);
        public Task<bool> InsertUltimoContactoAsync(BhUltimoContactoModel contacto);
        public Task<bool> UpdateUltimoContactoAsync(BhUltimoContactoModel contacto);
        public Task<bool> DeleteUltimoContactoBhAsync(int dni, string unidad);
        public Task<BhUltimoContactoModel> GetUltimoContactoByDniDetailAsync(int dni);
    }
}
