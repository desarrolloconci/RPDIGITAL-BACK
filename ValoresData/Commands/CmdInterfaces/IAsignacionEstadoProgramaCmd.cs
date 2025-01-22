using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IAsignacionEstadoProgramaCmd
    {
        public Task<IEnumerable<AsignacionEstadoProgramaModel>> GetAsignacionEstadoProgramaAsync();
        public Task<AsignacionEstadoProgramaModel> GetAsignacionEstadoProgramaDetailAsync(string dni, string unidad);
        public Task<bool> InsertAsignacionProgramaAsync(AsignacionEstadoProgramaModel asignacion);
        public Task<bool> UpdatAsignacionEstadoProgramaAsync(AsignacionEstadoProgramaModel asignacion);
        public Task<bool> DeleteAsignacionEstadoProgramaAsync(string dni, string unidad);
    }
}
