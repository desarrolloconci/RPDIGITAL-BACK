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
    public class RelEspBateriaService : IRelEspBateriaService
    {
        private readonly IRelEspBateriaCmd _relEspBateriaCmd;
        public RelEspBateriaService(IRelEspBateriaCmd relEspBateriaCmd)
        {
            _relEspBateriaCmd = relEspBateriaCmd;
        }
        public Task<IEnumerable<RelEspBateriasModel>> GetRelEspServiciosAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BhBateriasModel>> GetRelSolPractBhBateriaAsync(int usuario_id)
        {
            return await _relEspBateriaCmd.GetRelSolPractBhBateriaAsync(usuario_id);
        }
        public async Task<IEnumerable<BhBateriasPublicasModel>> BateriaPublicamodel()
        {
            return await _relEspBateriaCmd.BateriaPublicamodel();
        }
        public async Task<bool> InsertRelEspBaterias(RelBateriasEspModel model)
        {
            return await _relEspBateriaCmd.InsertRelEspBaterias(model);
        }
        public async Task<bool> DeleteRelEspBateriasAsync(int id)
        {
            return await _relEspBateriaCmd.DeleteRelEspBateriasAsync(id);
        }
    }
}
