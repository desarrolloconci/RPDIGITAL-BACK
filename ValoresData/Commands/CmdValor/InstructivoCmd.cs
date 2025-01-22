using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class InstructivoCmd : IInstructivoCmd
    {   private readonly DataBase3Context _dbContext;
        public InstructivoCmd(DataBase3Context dbContext)
        {
            _dbContext = dbContext;  
        }

        public async Task<IEnumerable<InstructivoModel>> GetInstructivoAsync()
        {
            return await _dbContext.V_BH_DESCRIPCIONES.ToListAsync();
        }

        public async Task<InstructivoModel> GetInstructivoByCodnomAsync(int id)
        {
            var result = await _dbContext.V_BH_DESCRIPCIONES.Where(e => e.id == id).
               Select(e => new InstructivoModel
               {
                   id = e.id,
                   ESTUDIO_ID = e.ESTUDIO_ID,
                   ESTUDIO_NOMBRE = e.ESTUDIO_NOMBRE,
                   PREPARACION = e.PREPARACION,
                   PLAZO_ID = e.PLAZO_ID,
                   PLAZO_NOMBRE = e.PLAZO_NOMBRE,
                   CRITERIO_ID = e.CRITERIO_ID,
                   CRITERIO_NOMBRE = e.CRITERIO_NOMBRE,
                   COMBINACION = e.COMBINACION,
                   DIAGNOSTICOS = e.DIAGNOSTICOS,
                   WARNING = e.WARNING,
                   INFORMACION = e.INFORMACION,
                   LINK = e.LINK,
                   CONSENTIMIENTO = e.CONSENTIMIENTO,
                   TIPOENTREGA_ID = e.TIPOENTREGA_ID,
                   TIPOENTREGA_NOMBRE = e.TIPOENTREGA_NOMBRE,
                   FORMACION = e.FORMACION,
                   BAJA = e.BAJA,
               }).FirstOrDefaultAsync();
            return result;
        }
    }
}
