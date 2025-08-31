using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CdmRp
{
    public class NnRelMotivoNoturnoCmd : INnRelMotivoNoTurnoCmd
    { private readonly DataBaseContext _dataBaseContext;
        public NnRelMotivoNoturnoCmd(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        public async Task<bool> DeletMotivoNoTurnoAsync(string idpedido, string metodo)
        {
            var borrar = await _dataBaseContext.NN_REL_MOTIVO_NO_TURNO
                .Where(e => e.idPedido == idpedido && e.metodo == metodo)
                .ToListAsync();
            if(borrar == null|| !borrar.Any())
            {
                return false;
            }
            _dataBaseContext.NN_REL_MOTIVO_NO_TURNO.RemoveRange(borrar);
            await _dataBaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletMotivoNoTurnoUnitarioAsync(string idEstudio, string idPedido)
        {
            var borrar = await _dataBaseContext.NN_REL_MOTIVO_NO_TURNO
               .FirstOrDefaultAsync(e => e.idPedido == idPedido && e.idEstudio == idEstudio);
               
            if (borrar is null )
            {
                return false;
            }
            _dataBaseContext.NN_REL_MOTIVO_NO_TURNO.Remove(borrar);
            await _dataBaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<NnRelMotivoNoTurnoModel>> GetRelMotivoNoTurnoAsync()
        {
            return await _dataBaseContext.NN_REL_MOTIVO_NO_TURNO.ToListAsync();
        }

        public async Task<bool> InsertMotivoNoTurnoAsync(NnRelMotivoNoTurnoModel motivoNoTurnoModel)
        {
            _dataBaseContext.NN_REL_MOTIVO_NO_TURNO.Add(motivoNoTurnoModel);
            await _dataBaseContext.SaveChangesAsync();
            if (motivoNoTurnoModel != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> InsertMotivoNoTurnoAsyncVarios(NnRelMotivoNoTurnoModel motivoNoTurnoModel)
        {
            var estudios = await _dataBaseContext.BEALTH_SOLPRACT_P_MANUAL
                           .Where(e => e.IDPEDIDO == motivoNoTurnoModel.idPedido && e.METODOPRACTICA == motivoNoTurnoModel.metodo)
                           .Select(e => e.IDESTUDIO).ToListAsync();
            foreach (var estudio in estudios)
            {
                _dataBaseContext.NN_REL_MOTIVO_NO_TURNO.Add(new NnRelMotivoNoTurnoModel
                {
                    idPedido = motivoNoTurnoModel.idPedido,
                    idEstudio = estudio,
                    metodo = motivoNoTurnoModel.metodo,
                    idMotivo = motivoNoTurnoModel.idMotivo,
                    idUsuario = motivoNoTurnoModel.idUsuario,
                    Creado = motivoNoTurnoModel.Creado,
                });
            }
            await _dataBaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMotivoNoTurnoAsync(NnRelMotivoNoTurnoModel motivoNoTurnoModel)
        {
            var noturno = await _dataBaseContext.NN_REL_MOTIVO_NO_TURNO
                 .FirstOrDefaultAsync(e => e.idPedido == motivoNoTurnoModel.idPedido && e.idEstudio == motivoNoTurnoModel.idEstudio);
            if (noturno != null)
            {
                noturno.idPedido=motivoNoTurnoModel.idPedido;
                noturno.idEstudio=motivoNoTurnoModel.idEstudio;
                noturno.metodo=motivoNoTurnoModel.metodo;
                noturno.idMotivo = motivoNoTurnoModel.idMotivo;
                noturno.idUsuario = motivoNoTurnoModel.idUsuario;
                noturno.Creado = motivoNoTurnoModel.Creado;
                await _dataBaseContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
