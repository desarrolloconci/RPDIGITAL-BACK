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
    public class AuxPracticasCmd : IAuxPracticasCmd
    {
        private readonly DataBaseContext _context;
        public AuxPracticasCmd(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AuxPracticasModel>> GetAuxPracticasAsync()
        {
            return await _context.V_AUX_PRACTICAS.ToListAsync();
        }

        public async Task<AuxPracticasModel> GetAuxPracticasByCodNomAsync(string codnom)
        {
            var result = await _context.V_AUX_PRACTICAS.Where(e => e.PRACTICA_CODNOM == codnom).
                Select(e => new AuxPracticasModel
                {   ID = e.ID,
                    ESTUDIO_ID = e.ESTUDIO_ID,
                    ESTUDIO_NOMBRE = e.ESTUDIO_NOMBRE,
                    PRACTICA_ID = e.PRACTICA_ID,
                    PRACTICA_NOMBRE = e.PRACTICA_NOMBRE,
                    PRACTICA_CODIGO = e.PRACTICA_CODIGO,
                    PRACTICA_CODNOM = e.PRACTICA_CODNOM,
                }).FirstOrDefaultAsync();
            return result;

        }
    }
}
