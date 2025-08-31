using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IObservacionesPacientesBhCmd
    {
        public Task<IEnumerable<ObservacionesPacientesBhModel>> GetObservacionesPacientesBhAsync();
        public Task<ObservacionesPacientesBhModel> GetObservacionesPacientesBhADetailAsync(string dni, string unidad);
        public Task<bool> InsertObservacionesPacientesBhsync(ObservacionesPacientesBhModel Observacion);
        public Task<bool> UpdatObservacionesPacientesBhAsync(ObservacionesPacientesBhModel Observacion);
        public Task<bool> DeleteObservacionesPacientesBhAsync(string dni, string unidad);
        public Task<ObservacionesPacientesBhModel> GetObservacionesPacientesBhAByDniDetailAsync(string dni);
    }
}
