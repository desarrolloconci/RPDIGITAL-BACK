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
        public async Task<bool> DeleteObservacionesPacientesBhAsync(string dni, string unidad)
        {
            return await _IObservacionesPacientesBhCmd.DeleteObservacionesPacientesBhAsync(dni,unidad);
        }

        public async Task<ObservacionesPacientesBhModel> GetObservacionesPacientesBhADetailAsync(string dni, string unidad)
        {
            return await _IObservacionesPacientesBhCmd.GetObservacionesPacientesBhADetailAsync(dni,unidad);
        }

        public async Task<IEnumerable<ObservacionesPacientesBhModel>> GetObservacionesPacientesBhAsync()
        {
            return await _IObservacionesPacientesBhCmd.GetObservacionesPacientesBhAsync();
        }

        public async Task<bool> ManageObservacionesPacientesBhsync(ObservacionesPacientesBhModel Observacion)
        {
            var obs = await _IObservacionesPacientesBhCmd.GetObservacionesPacientesBhADetailAsync(Observacion.dni, Observacion.unidad);


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
