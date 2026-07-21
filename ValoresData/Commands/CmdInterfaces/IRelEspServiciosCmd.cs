using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;
using ValorModels.Models;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IRelEspServiciosCmd
    {
        public Task<IEnumerable<RelEspServiciosModel>> GetRelEspServiciosAsync();

        public Task<IEnumerable<RelSolPractBhServicioSolModel>> GetRelSolPractBhServicioSolAsync(int usuario_id);

        public Task<List<RelEspServiciosModel>> GetRelEspServiciosDetailsAsync(int usuario_id);

        public Task<bool> InsertRelEspServicios(RelEspServicioModel model);

        public Task<RelEspServicioModel> GetRelEspServiciosByIdAsync(int id);

        public Task<bool> DeleteRelEspServiciosAsync(int id);

    }
}
