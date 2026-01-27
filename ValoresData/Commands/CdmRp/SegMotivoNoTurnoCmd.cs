using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CdmRp
{
    public class SegMotivoNoTurnoCmd : ISegMotivoNoTurnoCmd
    {
        private readonly DataBaseContext _context;
        public SegMotivoNoTurnoCmd(DataBaseContext context)
        {
            _context = context;
        }
       
        public async Task<IEnumerable<SegMotivoNoTurnoModel>> GetSegMotivoNoTurnoAsync()
        {
            return await _context.SEG_MOTIVO_NO_TURNO.Where(e=> e.baja == false).ToListAsync();
        }
    }
}
