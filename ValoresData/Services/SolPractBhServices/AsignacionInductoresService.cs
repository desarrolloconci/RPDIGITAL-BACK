using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhServices
{
    public class AsignacionInductoresService : IAsignacionInductoresService
    {
        private readonly IAsignacionInductoresCmd _asignacionInductoresCmd;
        public AsignacionInductoresService(IAsignacionInductoresCmd asignacionInductoresCmd)
        {
            _asignacionInductoresCmd = asignacionInductoresCmd;
        }
        public async Task<bool> DeleteAsignacionInductoresAsync(string dni, string unidad)
        {
            return await _asignacionInductoresCmd.DeleteAsignacionInductoresAsync(dni, unidad);
        }

        public async Task<AsignacionInductoresModel> GetAsignacionInductoresADetailAsync(string dni, string unidad)
        {
            return await _asignacionInductoresCmd.GetAsignacionInductoresADetailAsync(dni, unidad);
        }

        public async Task<IEnumerable<AsignacionInductoresModel>> GetAsignacionInductoresAsync()
        {
            return await _asignacionInductoresCmd.GetAsignacionInductoresAsync();
        }

        public async Task<bool> InsertAsignacionInductoresAsync(AsignacionInductoresModel asignacion)
        {
            var asig = await _asignacionInductoresCmd.GetAsignacionInductoresADetailAsync(asignacion.DNI, asignacion.UNIDAD);
            if (asig == null) { 
                return await _asignacionInductoresCmd.InsertAsignacionInductoresAsync(asignacion); }

            return await _asignacionInductoresCmd.UpdatAsignacionInductoresAsync(asignacion);
        }

        public async Task<bool> UpdatAsignacionInductoresAsync(AsignacionInductoresModel asignacion)
        {
            return await _asignacionInductoresCmd.UpdatAsignacionInductoresAsync(asignacion);
        }
    }
}
