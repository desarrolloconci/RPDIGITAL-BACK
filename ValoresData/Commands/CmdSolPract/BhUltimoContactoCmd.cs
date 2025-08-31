using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdSolPract
{
    public class BhUltimoContactoCmd : IBhUltimoContactoCmd
    {
        private readonly DataBaseContext _context;
        public BhUltimoContactoCmd(DataBaseContext context)
        {
            _context = context;   
        }

        public async Task<bool> DeleteUltimoContactoBhAsync(int dni, string unidad)
        {

            var contacto = await GetUltimoContactoADetailAsync(dni, unidad);
            if (contacto is null)
            {
                return false;
            }
            _context.BH_ULTIMO_CONTACTO.Remove(contacto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BhUltimoContactoModel> GetUltimoContactoADetailAsync(int dni, string unidad)
        {
            return await _context.BH_ULTIMO_CONTACTO.Where(e => e.dni == dni && e.unidad == unidad).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BhUltimoContactoModel>> GetUltimoContactoAsync()
        {
            return await _context.BH_ULTIMO_CONTACTO.ToListAsync();
        }

        public async Task<BhUltimoContactoModel> GetUltimoContactoByDniDetailAsync(int dni)
        {
            return await _context.BH_ULTIMO_CONTACTO.FirstOrDefaultAsync(e => e.dni == dni);
        }

        public async Task<bool> InsertUltimoContactoAsync(BhUltimoContactoModel contacto)
        {
            if (contacto == null)
            {
                return false;
            }
            _context.BH_ULTIMO_CONTACTO.Add(contacto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUltimoContactoAsync(BhUltimoContactoModel contacto)
        {
            var contact = await GetUltimoContactoADetailAsync(contacto.dni, contacto.unidad);
            if (contact != null)
            {
                contact.dni = contacto.dni;
                contact.unidad = contacto.unidad;
                contact.ult_fecha = contacto.ult_fecha;
                contact.usuario = contacto.usuario;
                contact.creado = contacto.creado;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
