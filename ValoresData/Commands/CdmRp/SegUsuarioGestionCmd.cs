using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class SegUsuarioGestionCmd : ISegUsuarioGestionCmd
    {
        private readonly DataBaseContext _context;
        public SegUsuarioGestionCmd(DataBaseContext context)
        {
            _context=context;
        }
        public async Task<IEnumerable<SegUsuarioGestionModel>> GetSegUsuarioGestion(SegUsuarioGestionModel model)
        {
            var resul= await _context.SEG_USUARIO_GESTION.Where(e => e.idEstudio == model.idEstudio && e.idPedido == e.idPedido).ToListAsync();
            return resul;
        }
        public async Task<bool> InsertSegUsuarioGestionVarios(SegUsuarioGestionModel model)
        {
            if (model == null)
                return false;

            var estudios = await _context.V_BEALTH_SOLPRAC
                .Where(x => x.IDPEDIDO == model.idPedido && x.METODOOK == model.metodoOk)
                .Select(x => x.IDESTUDIO)
                .ToListAsync();

            if (!estudios.Any())
                return false;

            var entidades = estudios.Select(idestudio => new SegUsuarioGestionModel
            {
                idPedido = model.idPedido,
                idEstudio = idestudio,
                metodoOk = model.metodoOk,
                fecha = model.fecha,
                idUsuario = model.idUsuario

            }).ToList();

            _context.SEG_USUARIO_GESTION.AddRange(entidades);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> InsertSegUsuarioGestionAsync(SegUsuarioGestionModel model)
        {
            if (model is null)
                return false;

            _context.SEG_USUARIO_GESTION.Add(model);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateSegUsuarioGestionAsync(SegUsuarioGestionModel model)
        {
            var entity = await _context.SEG_USUARIO_GESTION
                .FirstOrDefaultAsync(e => e.idPedido == model.idPedido && e.idEstudio == model.idEstudio);
            if (entity != null)
            {
                entity.idPedido = model.idPedido;
                entity.idEstudio = model.idEstudio;
                entity.metodoOk = model.metodoOk;
                entity.idUsuario = model.idUsuario;
                entity.fecha = model.fecha;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateSegUsuarioGestionVarios(SegUsuarioGestionModel model)
        {
            if (model == null)
                return false;

            var estudios = await _context.V_BEALTH_SOLPRAC
                .Where(x => x.IDPEDIDO == model.idPedido && x.METODOOK == model.metodoOk)
                .Select(x => x.IDESTUDIO)
                .ToListAsync();

            if (!estudios.Any())
                return false;

            
            var entidades = await _context.SEG_USUARIO_GESTION
                .Where(e => e.idPedido == model.idPedido && estudios.Contains(e.idEstudio))
                .ToListAsync();

            if (!entidades.Any())
                return false;

            foreach (var entity in entidades)
            {
                entity.metodoOk = model.metodoOk;
                entity.idUsuario = model.idUsuario;
                entity.fecha = model.fecha;
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
