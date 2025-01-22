using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models;
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

        public async Task<bool> InsertRelSolPractAsync(RelSolPractModel relSolPractModel)
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
                relsol.turno_id = relSolPractModel.turno_id;
                relsol.metodoOK = relSolPractModel.metodoOK;
                relsol.inductor= relSolPractModel.inductor;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> InsertRelSolPractAsyncVarios(RelSolPractModel relSolPractModel)
        {           
            var estudios = _context.V_BEALTH_SOLPRAC
                .Where(x => x.IDPEDIDO == relSolPractModel.idPedido && x.METODOOK== relSolPractModel.metodoOK)
                .Select(x => x.IDESTUDIO) 
                .ToList();

            foreach (var idEstudio in estudios)
            {               
               _context.REL_SOL_PRACT.Add(new RelSolPractModel
                {
                    idPedido = relSolPractModel.idPedido,
                    idEstudio = idEstudio, 
                    estadoPrograma = relSolPractModel.estadoPrograma,
                    estadoTurno = relSolPractModel.estadoTurno,
                    fechaGestion = relSolPractModel.fechaGestion,
                    observaciones = relSolPractModel.observaciones,
                    creado = relSolPractModel.creado,
                    usuario = relSolPractModel.usuario,
                    turno_id = relSolPractModel.turno_id,
                    metodoOK = relSolPractModel.metodoOK,
                    inductor=relSolPractModel.inductor,
                });
            }
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletRelSolPractTotalAsync(string idpedido, string metodoOK)
        {
            
            var entities = await _context.REL_SOL_PRACT
                .Where(e => e.idPedido == idpedido && e.metodoOK == metodoOK)
                .ToListAsync();
           
            if (entities == null || !entities.Any())
            {
                return false;
            }
            _context.REL_SOL_PRACT.RemoveRange(entities);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletRelSolPractUnitarioAsync(string idEstudio,string idPedido)
        {
            var entity = await _context.REL_SOL_PRACT
         .FirstOrDefaultAsync(e => e.idEstudio == idEstudio && e.idPedido == idPedido);

            if (entity is null)
            {
                return false;
            }

            _context.REL_SOL_PRACT.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
        

}

