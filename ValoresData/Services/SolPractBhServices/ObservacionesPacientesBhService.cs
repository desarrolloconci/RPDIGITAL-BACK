using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Commands.CmdSolPract;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhServices
{
    public class ObservacionesPacientesBhService : IObservacionesPacientesBhService
    {
        private readonly IObservacionesPacientesBhCmd _IObservacionesPacientesBhCmd;
        public ObservacionesPacientesBhService(IObservacionesPacientesBhCmd IObservacionesPacientesBhCmd)
        {
            _IObservacionesPacientesBhCmd = IObservacionesPacientesBhCmd;
        }
        public async Task<bool> DeleteObservacionesPacientesBhAsync(int id)
        {
            return await _IObservacionesPacientesBhCmd.DeleteObservacionesPacientesBhAsync(id);
        }

        public async Task<ObservacionesPacientesBhModel> GetObservacionesPacientesBhADetailAsync(int id)
        {
            return await _IObservacionesPacientesBhCmd.GetObservacionesPacientesBhADetailAsync(id);
        }

        public async Task<IEnumerable<ObservacionesPacientesBhModel>> GetObservacionesPacientesBhAsync()
        {
            return await _IObservacionesPacientesBhCmd.GetObservacionesPacientesBhAsync();
        }

        public async Task<bool> ManageObservacionesPacientesBhsync(ObservacionesPacientesBhModel Observacion)
        {
            var obs = await _IObservacionesPacientesBhCmd.GetObservacionesPacientesBhAByDniDetailAsync(Observacion.dni);


            if (obs == null)
            {
                await _IObservacionesPacientesBhCmd.InsertObservacionesPacientesBhsync(Observacion);
                return true;
            }
            await _IObservacionesPacientesBhCmd.UpdatObservacionesPacientesBhAsync(Observacion);
            return false;
        }

        public async Task<bool> UpdatObservacionesPacientesBhAsync(ObservacionesPacientesBhModel Observacion)
        {
            return await _IObservacionesPacientesBhCmd.UpdatObservacionesPacientesBhAsync(Observacion);
        }

        public async Task<ObservacionesPacientesBhModel> GetObservacionesPacientesBhAByDniDetailAsync(string dni)
        {
            return await _IObservacionesPacientesBhCmd.GetObservacionesPacientesBhAByDniDetailAsync(dni);
        }
    }
}
