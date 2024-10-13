using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdSolPract
{
    public class RelSolPractBhUtilsCmd : IRelSolPractBhUtilsCmd
    {
        private readonly DataBaseContext _dbContext;
        public RelSolPractBhUtilsCmd(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<RelSolPractBhMetodoModel>> GetRelSolPractBhMetodoAsync()
        {
            return await _dbContext.V_SolPractBhMetodo.ToListAsync();
        }

        public async Task<IEnumerable<RelSolPractBhUnidadModel>> GetRelSolPractBhUnidadAsync()
        {
            return await _dbContext.V_SolPractBhUnidad.ToListAsync();
        }
        public async Task<IEnumerable<RelSolPractBhServicioSolModel>> GetRelSolPractBhServicioSolAsync()
        {
            return await _dbContext.V_SolPractBhEspecialidad.ToListAsync();
        }
        public async Task<IEnumerable<RelSolPractBhOsModel>> GetRelSolPractBhOsAsync()
        {
            return await _dbContext.V_SolPractBhOs.ToListAsync();
        }
        public async Task<IEnumerable<RelSolPractBhInductoresModel>> GetRelSolPractBhInductoresAsync()
        {
            return await _dbContext.V_SolPractBhInductores.ToListAsync();
        }
        public async Task<IEnumerable<RelSolPractBhEstadoProgramaModel>> GetRelSolPractBhEstadoProgramaAsync()
        {
            return await _dbContext.Bh_Estado_Programa.ToListAsync();
        }
        public async Task<IEnumerable<RelSolPractBhEstadoTurnoModel>> GetRelSolPractBhEstadoTurnoAsync()
        {
            return await _dbContext.Bh_Estado_Turno.ToListAsync();
        }
    }
}
