using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.RpInterfaces;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Services.RpServices
{
    public class NnRelMotivoNoTurnoService : INnRelMotivoNoTurnoService
    { private readonly INnRelMotivoNoTurnoCmd _NnRelMotivoNoturnoCmd;
        public NnRelMotivoNoTurnoService(INnRelMotivoNoTurnoCmd nnRelMotivoNoTurnoCmd)
        {
            _NnRelMotivoNoturnoCmd = nnRelMotivoNoTurnoCmd;
        }
        public Task<bool> DeletMotivoNoTurnoAsync(string idpedido, string metodo)
        {
           return _NnRelMotivoNoturnoCmd.DeletMotivoNoTurnoAsync(idpedido, metodo);
        }

        public Task<bool> DeletMotivoNoTurnoUnitarioAsync(string idEstudio, string idPedido)
        {
            return _NnRelMotivoNoturnoCmd.DeletMotivoNoTurnoUnitarioAsync(idEstudio, idPedido);
        }

        public async Task<IEnumerable<NnRelMotivoNoTurnoModel>> GetRelMotivoNoTurnoAsync()
        {
           return await _NnRelMotivoNoturnoCmd.GetRelMotivoNoTurnoAsync();
        }

        public async Task<bool> InsertMotivoNoTurnoAsync(NnRelMotivoNoTurnoModel motivoNoTurnoModel)
        {
            try
            {
                if (motivoNoTurnoModel.metodo == "Laboratorio")
                {

                    return await _NnRelMotivoNoturnoCmd.InsertMotivoNoTurnoAsyncVarios(motivoNoTurnoModel);
                }
                else
                    return await _NnRelMotivoNoturnoCmd.InsertMotivoNoTurnoAsync(motivoNoTurnoModel);
            }
            catch (Exception ex) { Console.WriteLine(ex); return false; }
        }

        public async Task<bool> InsertMotivoNoTurnoAsyncVarios(NnRelMotivoNoTurnoModel motivoNoTurnoModel)
        {
            return await _NnRelMotivoNoturnoCmd.InsertMotivoNoTurnoAsyncVarios(motivoNoTurnoModel);
        }

        public async Task<bool> UpdateMotivoNoTurnoAsync(NnRelMotivoNoTurnoModel motivoNoTurnoModel)
        {
            if (motivoNoTurnoModel == null)
            {
                throw new ArgumentNullException(nameof(motivoNoTurnoModel), "The model cannot be null.");
            }

            try
            {
                return await _NnRelMotivoNoturnoCmd.UpdateMotivoNoTurnoAsync(motivoNoTurnoModel);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("An error occurred while updating the record.", ex);
            }
        }
    }
}
