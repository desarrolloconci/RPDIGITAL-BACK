using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhServices
{
    public class RelSolPractBhUtilsService : IRelSolPractBhUtilsService
    {
        private readonly IRelSolPractBhUtilsCmd _relSolPractBhMetodoCmd;
        public RelSolPractBhUtilsService(IRelSolPractBhUtilsCmd relSolPractBhMetodoCmd)
        {
            _relSolPractBhMetodoCmd = relSolPractBhMetodoCmd;
        }
        public async Task<IEnumerable<RelSolPractBhMetodoModel>> GetRelSolPractBhMetodoAsync()
        {
            return await _relSolPractBhMetodoCmd.GetRelSolPractBhMetodoAsync();
        }
        public async Task<IEnumerable<RelSolPractBhUnidadModel>> GetRelSolPractBhUnidadAsync()
        {
            return await _relSolPractBhMetodoCmd.GetRelSolPractBhUnidadAsync();
        }

        public async Task<IEnumerable<RelSolPractBhServicioSolModel>> GetRelSolPractBhServicioSolAsync()
        {
            return await _relSolPractBhMetodoCmd.GetRelSolPractBhServicioSolAsync();
        }
        public async Task<IEnumerable<RelSolPractBhOsModel>> GetRelSolPractBhOsAsync()
        {
            return await _relSolPractBhMetodoCmd.GetRelSolPractBhOsAsync();
        }

        public async Task<IEnumerable<RelSolPractBhInductoresModel>> GetRelSolPractBhInductoresAsync()
        {
            return await _relSolPractBhMetodoCmd.GetRelSolPractBhInductoresAsync();
        }
        public async Task<IEnumerable<RelSolPractBhEstadoProgramaModel>> GetRelSolPractBhEstadoProgramaAsync()
        {
            return await _relSolPractBhMetodoCmd.GetRelSolPractBhEstadoProgramaAsync();
        }
        public async Task<IEnumerable<RelSolPractBhEstadoTurnoModel>> GetRelSolPractBhEstadoTurnoAsync()
        {
            return await _relSolPractBhMetodoCmd.GetRelSolPractBhEstadoTurnoAsync();
        }
        public async Task<IEnumerable<NnMotivoNoTurnoModel>> GetMotivoNoTurnoAsync()
        {
            return await _relSolPractBhMetodoCmd.GetMotivoNoTurnoAsync();
        }

        public async Task<IEnumerable<ObrasSocialesLaboModel>> GeOsLaboAsync()
        {
            return await _relSolPractBhMetodoCmd.GeOsLaboAsync();
        }
    }
}
