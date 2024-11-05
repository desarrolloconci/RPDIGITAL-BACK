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
    public class RelSolPractCmd : IReslSolPractCmd
    {
        private readonly DataBaseContext _context;
        public RelSolPractCmd(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RelSolPractModel>> GetRelSolPractAsync()
        {
            return await _context.REL_SOL_PRACT.ToListAsync();
        }

        public async Task<bool> InsertProgramasync(RelSolPractModel relSolPractModel)
        {
            _context.REL_SOL_PRACT.Add(relSolPractModel);
            await _context.SaveChangesAsync();
            if (relSolPractModel != null)
            {
                return true;
            }
            return false;

        }
        public async Task<RelSolPractModel> GetRelSolAsyncById(int id)
        {
            return await _context.REL_SOL_PRACT.FindAsync(id);
        }

        public async Task<bool> UpdateRelSolAsync(RelSolPractModel relSolPractModel)
        {
            var relsol = await _context.REL_SOL_PRACT.FindAsync(relSolPractModel.id);
            if (relsol != null)
            {
                relsol.idPedido = relSolPractModel.idPedido;
                relsol.idEstudio = relSolPractModel.idEstudio;
                relsol.estadoPrograma = relSolPractModel.estadoPrograma;
                relsol.estadoTurno = relSolPractModel.estadoTurno;
                relsol.fechaGestion = relSolPractModel.fechaGestion;
                relsol.observaciones = relSolPractModel.observaciones;
                relsol.creado = relSolPractModel.creado;
                relsol.usuario = relSolPractModel.usuario;
                relsol.turno_id= relSolPractModel.turno_id;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
