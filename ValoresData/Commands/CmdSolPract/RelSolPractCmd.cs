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

        public async Task<IEnumerable<RelSolPractModel>>GetRelSolVariosAsync(string idPedido,string idEstudio)
        {
            return await _context.REL_SOL_PRACT.Where(e=> e.idPedido ==idPedido && e.idEstudio==idEstudio).ToListAsync();
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
                relsol.inductor = relSolPractModel.inductor;
                relsol.tur_fecha = relSolPractModel.tur_fecha;
                relsol.servicio_id= relSolPractModel.servicio_id;
                relsol.serv_nombre= relSolPractModel.serv_nombre;

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
                    tur_fecha= relSolPractModel.tur_fecha,
                    metodoOK = relSolPractModel.metodoOK,
                    inductor=relSolPractModel.inductor,
                    serv_nombre=relSolPractModel.serv_nombre,
                    servicio_id=relSolPractModel.servicio_id,
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

        public async Task<IEnumerable<SolPractBhPedidoManualModel>> GetRpVinculadosATurnoAsync(int turnoId)
        {
            var relaciones = await _context.REL_SOL_PRACT
                .Where(e => e.turno_id == turnoId)
                .ToListAsync();

            if (!relaciones.Any())
            {
                return Enumerable.Empty<SolPractBhPedidoManualModel>();
            }

            var idPedidos = relaciones.Select(r => r.idPedido).Distinct().ToList();
            var idEstudioNums = relaciones.Select(r => r.IDESTUDIO_NUM).Distinct().ToList();

            return await _context.BEALTH_SOLPRACT_P_MANUAL_OK
                .Where(b => idPedidos.Contains(b.IDPEDIDO) && idEstudioNums.Contains(b.IDESTUDIO_NUM))
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetTurnoIdsConPedidoAsync(List<int> turnoIds)
        {
            return await _context.REL_SOL_PRACT
                .Where(e => e.turno_id.HasValue && turnoIds.Contains(e.turno_id.Value))
                .Select(e => e.turno_id.Value)
                .Distinct()
                .ToListAsync();
        }
    }
        

}

