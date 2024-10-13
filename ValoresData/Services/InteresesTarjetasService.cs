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
    public class InteresesTarjetasService : IInteresTarjetasServices
    {
        private readonly IInteresesTarjetasCmd _interesesTarjetasCmd;
        public InteresesTarjetasService(IInteresesTarjetasCmd interesesTarjetasCmd)
        {
            _interesesTarjetasCmd = interesesTarjetasCmd;
        }
        public async Task<bool> DeleteInteresAsync(int id)
        {
           return await _interesesTarjetasCmd.DeleteInteresAsync(id);
        }

        public async Task<InteresesTarjetasModel> GetInteresAsyncById(int id)
        {
            return await _interesesTarjetasCmd.GetInteresAsyncById(id);
        }

        public async Task<IEnumerable<InteresesTarjetasModel>> GetInteresesAsync()
        {
           return await _interesesTarjetasCmd.GetInteresesAsync();
        }

        public async Task<bool> InsertInteresAsync(InteresesTarjetasModel intereses)
        {
            return await _interesesTarjetasCmd.InsertInteresAsync(intereses);
        }

        public async Task<bool> UpdateInteresAsync(InteresesTarjetasModel intereses)
        {
            return await _interesesTarjetasCmd.UpdateInteresAsync(intereses);
        }
    }
}
