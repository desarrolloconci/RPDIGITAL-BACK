using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhInterfaces
{
    public interface IRelSolPractBhUtilsService
    {
        public Task<IEnumerable<RelSolPractBhMetodoModel>> GetRelSolPractBhMetodoAsync();
        public Task<IEnumerable<RelSolPractBhUnidadModel>> GetRelSolPractBhUnidadAsync();
        public Task<IEnumerable<RelSolPractBhServicioSolModel>> GetRelSolPractBhServicioSolAsync();
        public Task<IEnumerable<RelSolPractBhOsModel>> GetRelSolPractBhOsAsync();
        public Task<IEnumerable<RelSolPractBhInductoresModel>> GetRelSolPractBhInductoresAsync();
        public Task<IEnumerable<RelSolPractBhEstadoProgramaModel>> GetRelSolPractBhEstadoProgramaAsync();
        public Task<IEnumerable<RelSolPractBhEstadoTurnoModel>> GetRelSolPractBhEstadoTurnoAsync();
        public Task<IEnumerable<NnMotivoNoTurnoModel>> GetMotivoNoTurnoAsync();
    }
}
