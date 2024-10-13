using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IInteresTarjetasServices
    {
        public Task<IEnumerable<InteresesTarjetasModel>> GetInteresesAsync();
        public Task<InteresesTarjetasModel> GetInteresAsyncById(int id);
        public Task<bool> InsertInteresAsync(InteresesTarjetasModel intereses);
        public Task<bool> UpdateInteresAsync(InteresesTarjetasModel intereses);
        public Task<bool> DeleteInteresAsync(int id);
    }
}
