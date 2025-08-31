using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhServices
{
    public class UltimoContactoService : IBhUltimoContactoService
    {
        private readonly IBhUltimoContactoCmd _ultimo;
        public UltimoContactoService(IBhUltimoContactoCmd ultimo)
        {
            _ultimo = ultimo;
        }
        public async Task<bool> DeleteUltimoContactoBhAsync(int dni, string unidad)
        {
            return await _ultimo.DeleteUltimoContactoBhAsync(dni, unidad);
        }

        public async Task<BhUltimoContactoModel> GetUltimoContactoADetailAsync(int dni, string unidad)
        {
           return await _ultimo.GetUltimoContactoADetailAsync(dni,unidad);
        }

        public async Task<IEnumerable<BhUltimoContactoModel>> GetUltimoContactoAsync()
        {
            return await _ultimo.GetUltimoContactoAsync();
        }

        public async Task<BhUltimoContactoModel> GetUltimoContactoByDniDetailAsync(int dni)
        {
            return await _ultimo.GetUltimoContactoByDniDetailAsync(dni);
        }

        public async Task<bool> InsertUltimoContactoAsync(BhUltimoContactoModel contacto)
        {
            var contact = await _ultimo.GetUltimoContactoADetailAsync(contacto.dni, contacto.unidad);


            if (contact == null)
            {
                await _ultimo.InsertUltimoContactoAsync(contacto);
                return true;
            }
            await _ultimo.UpdateUltimoContactoAsync(contacto);
            return false;
        }

        public async Task<bool> UpdateUltimoContactoAsync(BhUltimoContactoModel contacto)
        {
            return await _ultimo.UpdateUltimoContactoAsync(contacto);  
        }
    }
}
