using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CdmRp
{
    public class RelSegMotivoNoTurnoCmd : IRelSegMotivoNoTurnoCmd
    {
        private readonly DataBaseContext _context;
        public RelSegMotivoNoTurnoCmd(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RelSegMotivoNoTurnoModel>> GetRelSegMotivoNoTurnoAsync()
        {
            return await _context.SEG_REL_MOTIVO_NO_TURNO.ToListAsync();
        }

        public async Task<IEnumerable<RelSegMotivoNoTurnoModel>> GetRelSegMotivoById(RelSegMotivoNoTurnoModel model)
        {
            var resul = await _context.SEG_REL_MOTIVO_NO_TURNO.Where(e => e.idEstudio == model.idEstudio && e.idPedido == e.idPedido).ToListAsync();
            return resul;
        }

        public async Task<bool> InsertRelSegMotivoNoTurnoAsync(RelSegMotivoNoTurnoModel model)
        {

            if (model is null)
                return false;

            _context.SEG_REL_MOTIVO_NO_TURNO.Add(model);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
        public async Task<bool> InsertRelSegMotivoNoVarios(RelSegMotivoNoTurnoModel model)
        {
            if (model == null)
                return false;

            var estudios = await _context.V_BEALTH_SOLPRAC
                .Where(x => x.IDPEDIDO == model.idPedido && x.METODOOK == model.MetodoOK)
                .Select(x => x.IDESTUDIO)
                .ToListAsync();

            if (!estudios.Any())
                return false;

            var entidades = estudios.Select(idestudio => new RelSegMotivoNoTurnoModel
            {
               idPedido=model.idPedido,
               idEstudio = idestudio,
               id_motivo_no_turno =model.id_motivo_no_turno,
               id_usuario=model.id_usuario,
               MetodoOK = model.MetodoOK,
               fecha= model.fecha
            }).ToList();

            _context.SEG_REL_MOTIVO_NO_TURNO.AddRange(entidades);

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateRelSegMotivoNoAsync(RelSegMotivoNoTurnoModel model)
        {
            var entity = await _context.SEG_REL_MOTIVO_NO_TURNO
                .FirstOrDefaultAsync(e => e.idPedido == model.idPedido && e.idEstudio == model.idEstudio);
            if (entity != null)
            {
                entity.idPedido = model.idPedido;
                entity.idEstudio = model.idEstudio;
                entity.MetodoOK = model.MetodoOK;
                entity.id_motivo_no_turno = model.id_motivo_no_turno;
                entity.id_usuario = model.id_usuario;
                entity.fecha = model.fecha;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateRelSegMotivoNoVarios(RelSegMotivoNoTurnoModel model)
        {
            if (model == null)
                return false;

            var estudios = await _context.V_BEALTH_SOLPRAC
                .Where(x => x.IDPEDIDO == model.idPedido && x.METODOOK == model.MetodoOK)
                .Select(x => x.IDESTUDIO)
                .ToListAsync();

            if (!estudios.Any())
                return false;


            var entidades = await _context.SEG_REL_MOTIVO_NO_TURNO
                .Where(e => e.idPedido == model.idPedido && estudios.Contains(e.idEstudio))
                .ToListAsync();

            if (!entidades.Any())
                return false;

            foreach (var entity in entidades)
            {
                entity.MetodoOK = model.MetodoOK;
                entity.id_usuario = model.id_usuario;
                entity.id_motivo_no_turno = model.id_motivo_no_turno;
                entity.fecha = model.fecha;
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
