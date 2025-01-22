using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
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
            try
            {
                if (relSolPractModel.metodoOK == "Laboratorio" || relSolPractModel.metodoOK == "Módulo Base"|| relSolPractModel.metodoOK == "Modulo Base")
                {
                    Console.WriteLine("ENTRE A LABO");
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
                throw new ArgumentNullException(nameof(relSolPractModel), "The model cannot be null.");
            }

            try
            {
                return await _cmd.UpdateRelSolAsync(relSolPractModel);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("An error occurred while updating the record.", ex);
            }
        }
        public async Task<RelSolPractModel> GetRelSolAsyncById(int id)
        {

            return await _cmd.GetRelSolAsyncById(id);
        }
        public async Task<bool> DeletRelSolPractTotalAsync(string idpedido, string metodoOK)
        {
            return await _cmd.DeletRelSolPractTotalAsync(idpedido, metodoOK);
        }
        public async Task<bool> DeletRelSolPractUnitarioAsync(string idEstudio, string idPedido)
        {
            return await _cmd.DeletRelSolPractUnitarioAsync(idEstudio, idPedido);
        }
    }

}
