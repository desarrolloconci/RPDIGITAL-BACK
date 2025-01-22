using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhInterfaces
{
    public interface IObservacionesPacientesBhService
    { 
            public Task<IEnumerable<ObservacionesPacientesBhModel>> GetObservacionesPacientesBhAsync();
            public Task<ObservacionesPacientesBhModel> GetObservacionesPacientesBhADetailAsync(int id);
            public Task<bool> ManageObservacionesPacientesBhsync(ObservacionesPacientesBhModel Observacion);
            public Task<bool> UpdatObservacionesPacientesBhAsync(ObservacionesPacientesBhModel Observacion);
            public Task<bool> DeleteObservacionesPacientesBhAsync(int id);
            public Task<ObservacionesPacientesBhModel> GetObservacionesPacientesBhAByDniDetailAsync(string dni);

    }
}
