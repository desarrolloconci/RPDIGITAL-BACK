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
    public class SolPractBhPedidoManualCmd : ISolPractBhPedidoManualCmd
    {
        private readonly DataBaseContext _context;
        public SolPractBhPedidoManualCmd(DataBaseContext context)
        {
            _context = context;
        }
        public Task<bool> DeletRelSolPractBhPedidoManualoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SolPractBhPedidoManualModel>> GetSolPractBhPedidoManualAsync()
        {
            return await _context.BEALTH_SOLPRACT_P_MANUAL.ToListAsync();
        }

        public async Task<SolPractBhPedidoManualModel> GetSolPractBhPedidoManuallAsyncById(int id)
        {
            return await _context.BEALTH_SOLPRACT_P_MANUAL.FindAsync(id);
        }

        public async Task<bool> InsertSolPractBhPedidoManualAsync(SolPractBhPedidoManualModel pedidomanual)
        {
            _context.BEALTH_SOLPRACT_P_MANUAL.Add(pedidomanual);
            await _context.SaveChangesAsync();
            if (pedidomanual != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateSolPractBhPedidoManualAsync(SolPractBhPedidoManualModel pedidomanual)
        {
            var relsol = await _context.BEALTH_SOLPRACT_P_MANUAL.FindAsync(pedidomanual.ID);
            if (relsol != null)
            {
                relsol.IDPEDIDO=pedidomanual.IDPEDIDO;
                relsol.IDTRATAMIENTO=pedidomanual.IDTRATAMIENTO;
                relsol.NOMBREBATERIA=pedidomanual.NOMBREBATERIA;
                relsol.FECHA=pedidomanual.FECHA;
                relsol.FECHACREACION=pedidomanual.FECHACREACION;
                relsol.DIAGNÓSTICO=pedidomanual.DIAGNÓSTICO;
                relsol.METODOPRACTICA = pedidomanual.METODOPRACTICA;
                relsol.IDESTUDIO=pedidomanual.IDESTUDIO;
                relsol.ESTUDIO=pedidomanual.ESTUDIO;
                relsol.DNI=pedidomanual.DNI;
                relsol.NOMBRE = pedidomanual.NOMBRE;
                relsol.IDOBRASOCIAL=pedidomanual.IDOBRASOCIAL;
                relsol.OBRASOCIAL = pedidomanual.OBRASOCIAL;
                relsol.NUMEROAFILIADO = pedidomanual.NUMEROAFILIADO;
                relsol.CELULAR = pedidomanual.CELULAR;
                relsol.EMAIL = pedidomanual.EMAIL;
                relsol.CODIGOPRESTADOR = pedidomanual.CODIGOPRESTADOR;
                relsol.PRESTADORQUEGENERASOLICITUD = pedidomanual.PRESTADORQUEGENERASOLICITUD;
                relsol.IDTURNO = pedidomanual.IDTURNO;
                relsol.FECHAHORA = pedidomanual.FECHAHORA;
                //relsol.FECHAHORAALTA = pedidomanual.FECHAHORAALTA;
                relsol.FECHAHORAGESTIONDEESTADO = pedidomanual.FECHAHORAGESTIONDEESTADO;
                relsol.USUARIOALTA = pedidomanual.USUARIOALTA;
                relsol.IDSERVICIOTURNO = pedidomanual.IDSERVICIOTURNO;
                relsol.SERVICIOTURNO = pedidomanual.SERVICIOTURNO;
                relsol.IDSERVICIOSOLICITUD = pedidomanual.IDSERVICIOSOLICITUD;
                relsol.SERVICIOSOLICITUD = pedidomanual.SERVICIOSOLICITUD;
                relsol.CODIGOPRESTADORDELTURNO = pedidomanual.CODIGOPRESTADORDELTURNO;
                relsol.PRESTADORDELTURNO = pedidomanual.PRESTADORDELTURNO;
                relsol.FECHAHORACONF = pedidomanual.FECHAHORACONF;
                relsol.FECHAHORAATENCION = pedidomanual.FECHAHORAATENCION;
                relsol.ESTADO = pedidomanual.ESTADO;
                relsol.USUARIOGESTIONOESTADO = pedidomanual.USUARIOGESTIONOESTADO;
                relsol.CONTACTACION = pedidomanual.CONTACTACION;
                relsol.MOTIVONOTURNO = pedidomanual.MOTIVONOTURNO;
                relsol.OBSERVACIONES = pedidomanual.OBSERVACIONES;
                relsol.CREADO = pedidomanual.CREADO;
                relsol.OBSERVACION_INTERNA = pedidomanual.OBSERVACION_INTERNA;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
