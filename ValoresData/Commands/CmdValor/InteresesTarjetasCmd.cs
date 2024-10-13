using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models;

namespace ValoresData.Commands.CmdValor
{
    public class InteresesTarjetasCmd : IInteresesTarjetasCmd
    {
        private readonly DataBaseContext _context;
        public InteresesTarjetasCmd(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteInteresAsync(int id)
        {
            var dbinteres = await GetInteresAsyncById(id);
            if (dbinteres is null)
            {
                return false;
            }
            _context.Intereses_tarjetas.Remove(dbinteres);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<InteresesTarjetasModel> GetInteresAsyncById(int id)
        {
            return await _context.Intereses_tarjetas.FindAsync(id);
        }

        public async Task<IEnumerable<InteresesTarjetasModel>> GetInteresesAsync()
        {
            return await _context.Intereses_tarjetas.ToListAsync();
        }

        public async Task<bool> InsertInteresAsync(InteresesTarjetasModel intereses)
        {
            _context.Intereses_tarjetas.Add(intereses);
            await _context.SaveChangesAsync();
            if (intereses != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateInteresAsync(InteresesTarjetasModel intereses)
        {
            var dbinteres = await _context.Intereses_tarjetas.FindAsync(intereses.id);
            if (dbinteres != null)
            {
                dbinteres.tarjeta = intereses.tarjeta;
                dbinteres.cuotas = intereses.cuotas;
                dbinteres.cuotas = intereses.cuotas;     
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
