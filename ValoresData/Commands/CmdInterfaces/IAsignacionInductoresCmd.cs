using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IAsignacionInductoresCmd
    {
        public Task<IEnumerable<AsignacionInductoresModel>> GetAsignacionInductoresAsync();
        public Task<AsignacionInductoresModel> GetAsignacionInductoresADetailAsync(string dni, string unidad);
        public Task<bool> InsertAsignacionInductoresAsync(AsignacionInductoresModel asignacion);
        public Task<bool> UpdatAsignacionInductoresAsync(AsignacionInductoresModel asignacion);
        public Task<bool> DeleteAsignacionInductoresAsync(string dni, string unidad);
    }
}
