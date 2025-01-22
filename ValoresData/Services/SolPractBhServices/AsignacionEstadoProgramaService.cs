using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Commands.CmdSolPract;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhServices
{
    public class AsignacionEstadoProgramaService : IAsignacionEstadoProgramaService
    {
        private readonly IAsignacionEstadoProgramaCmd _AsignacionEstadoProgramaCmd;
        public AsignacionEstadoProgramaService(IAsignacionEstadoProgramaCmd asignacionEstadoProgramaCmd)
        {
            _AsignacionEstadoProgramaCmd = asignacionEstadoProgramaCmd;
        }

        public async Task<bool> DeleteAsignacionEstadoProgramaAsync(string dni, string unidad)
        {
            return await _AsignacionEstadoProgramaCmd.DeleteAsignacionEstadoProgramaAsync(dni, unidad);
        }

        public async Task<IEnumerable<AsignacionEstadoProgramaModel>> GetAsignacionEstadoProgramaAsync()
        {
           return await _AsignacionEstadoProgramaCmd.GetAsignacionEstadoProgramaAsync();
        }

        public async Task<AsignacionEstadoProgramaModel> GetAsignacionEstadoProgramaDetailAsync(string dni, string unidad)
        {
           return await _AsignacionEstadoProgramaCmd.GetAsignacionEstadoProgramaDetailAsync(dni,unidad);
        }

        public async Task<bool> InsertAsignacionProgramaAsync(AsignacionEstadoProgramaModel asignacion)
        {
            var asig = await GetAsignacionEstadoProgramaDetailAsync(asignacion.dni, asignacion.unidad);
            if (asig == null)
            {
                return await _AsignacionEstadoProgramaCmd.InsertAsignacionProgramaAsync(asignacion);
            }

            return await _AsignacionEstadoProgramaCmd.UpdatAsignacionEstadoProgramaAsync(asignacion);
        }   
        public async  Task<bool> UpdatAsignacionEstadoProgramaAsync(AsignacionEstadoProgramaModel asignacion)
        {
            return await _AsignacionEstadoProgramaCmd.UpdatAsignacionEstadoProgramaAsync(asignacion);
        }
    }
}
