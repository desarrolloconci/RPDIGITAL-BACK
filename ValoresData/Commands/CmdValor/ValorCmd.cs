using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos;
using ValorModels.Models;

namespace ValoresData.Commands.CmdValor
{
    public class ValorCmd : IValorCmd
    {
        private readonly DataBaseContext _context;
        public ValorCmd(DataBaseContext context)
        {
            _context=context;
        }

        public async Task<IEnumerable<OsPlanDto>> GetOsAsync()
        {

            var result = await _context.OSPlanConCodOs
         .Select(e => new OsPlanDto
         {
             CodigoOs = e.codigoOS,
             Os = e.Os,
             codOs = e.codOs,
         })
         .Distinct()
         .ToListAsync();

            return result;

        }

        public async Task<IEnumerable<PlanDto>> GetPlaOs(int CodigoOS)
        {
            var result = await _context.OSPlanConCodOs
                .Where (e => e.codigoOS == CodigoOS)
                .Select(e => new PlanDto
                    {
                     Plan = e.Plan,
                     PlanId = e.PlanId,
                     })
                .Distinct()
                .ToListAsync();

            return result;

        }

        public async Task<ValorModel> GetValorDetails(int id)
        {
           return await _context.T_AUX_TCB.FindAsync(id);
        }

        public async Task<IEnumerable<ValorModel>> GetValoresAsync()
        {
            return await _context.T_AUX_TCB.Take(100).ToListAsync();
        }
    }
}
