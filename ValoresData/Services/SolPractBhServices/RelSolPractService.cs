using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Dtos;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhServices
{
    public class RelSolPractService : IRelSolPractService
    {
        private readonly IReslSolPractCmd _cmd;
        public RelSolPractService(IReslSolPractCmd cmd)
        {
            _cmd = cmd;
        }

        public async Task<IEnumerable<RelSolPractModel>> GetRelSolPractAsync()
        {
            return await _cmd.GetRelSolPractAsync();
        }

        public async Task<bool> InsertRelSolPractAsync(RelSolPractModel relSolPractModel)
        {
            var turno = await _cmd.GetRelSolVariosAsync(relSolPractModel.idPedido, relSolPractModel.idEstudio);
            if (turno != null && turno.Any()) { return true; }
            if (relSolPractModel.turno_id == 1) {
                relSolPractModel.turno_id = (int)(DateTime.UtcNow.Ticks % 1_000_000_000);
                  }
            try
            {
                if (relSolPractModel.metodoOK == "Laboratorio" || relSolPractModel.metodoOK == "Módulo Base"|| relSolPractModel.metodoOK == "Modulo Base")
                {
                   
                    return await _cmd.InsertRelSolPractAsyncVarios(relSolPractModel);
                }
                else
                    return await _cmd.InsertRelSolPractAsync(relSolPractModel);
            }
            catch (Exception ex) { Console.WriteLine(ex); return false; }
            
        }

        public async Task<bool> UpdateRelSolAsync(RelSolPractModel relSolPractModel)
        {
            if (relSolPractModel == null)
            {
                throw new ArgumentNullException(nameof(relSolPractModel), "El Modelo no puede ser nulo.");
            }

            try
            {
                return await _cmd.UpdateRelSolAsync(relSolPractModel);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("Ocurrio un error.", ex);
            }
        }
        public async Task<RelSolPractModel> GetRelSolAsyncById(int id)
        {

            return await _cmd.GetRelSolAsyncById(id);
        }
        public async Task <bool> DeleteUnificadoRelSolPractAsync(SEG_DESASOCIOARTURNO_DTO model)
        {
            try
            {
                if (model.metodoOK == "Laboratorio" || model.metodoOK == "Módulo Base" || model.metodoOK == "Modulo Base")
                {

                    return await _cmd.DeletRelSolPractTotalAsync(model.idPedido,model.metodoOK, model.usuario);
                }
                else
                    return await _cmd.DeletRelSolPractUnitarioAsync(model.idEstudio,model.idPedido, model.usuario);
            }
            catch (Exception ex) { Console.WriteLine(ex); return false; }
        }
        public async Task<bool> DeletRelSolPractTotalAsync(string idpedido, string metodoOK)
        {
            return await _cmd.DeletRelSolPractTotalAsync(idpedido, metodoOK);
        }
        public async Task<bool> DeletRelSolPractUnitarioAsync(string idEstudio, string idPedido)
        {
            return await _cmd.DeletRelSolPractUnitarioAsync(idEstudio, idPedido);
        }
        public async Task<IEnumerable<SolPractBhPedidoManualModel>> GetRpVinculadosATurnoAsync(int turnoId)
        {
            return await _cmd.GetRpVinculadosATurnoAsync(turnoId);
        }
        public async Task<Dictionary<int, bool>> GetTurnosConPedidoAsync(List<int> turnoIds)
        {
            var conPedido = (await _cmd.GetTurnoIdsConPedidoAsync(turnoIds)).ToHashSet();
            return turnoIds.Distinct().ToDictionary(id => id, id => conPedido.Contains(id));
        }
    }

}
