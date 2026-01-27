using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdSolPract
{
    public class RelSolPractBhUtilsCmd : IRelSolPractBhUtilsCmd
    {
        private readonly DataBaseContext _dbContext;
        private readonly DataBase3Context _dbContext3;
        public RelSolPractBhUtilsCmd(DataBaseContext dbContext, DataBase3Context dbContext3)
        {
            _dbContext = dbContext;
            _dbContext3 = dbContext3;
        }

        public async Task<IEnumerable<RelSolPractBhMetodoModel>> GetRelSolPractBhMetodoAsync()
        {
            _dbContext.Database.SetCommandTimeout(120);
            return await _dbContext.V_SolPractBhMetodo.ToListAsync();
        }

        public async Task<IEnumerable<RelSolPractBhUnidadModel>> GetRelSolPractBhUnidadAsync()
        {
            _dbContext.Database.SetCommandTimeout(120);
            return await _dbContext.V_SolPractBhUnidad.ToListAsync();
        }
        public async Task<IEnumerable<RelSolPractBhServicioSolModel>> GetRelSolPractBhServicioSolAsync()
        {
            _dbContext.Database.SetCommandTimeout(120);
            return await _dbContext.V_SolPractBhEspecialidad.ToListAsync();
        }
        public async Task<IEnumerable<RelSolPractBhOsModel>> GetRelSolPractBhOsAsync()
        {
            _dbContext.Database.SetCommandTimeout(120);
            return await _dbContext.V_SolPractBhOs.ToListAsync();
        }
        public async Task<IEnumerable<RelSolPractBhInductoresModel>> GetRelSolPractBhInductoresAsync()
        {
            _dbContext.Database.SetCommandTimeout(120);
            return await _dbContext.V_SolPractBhInductores.ToListAsync();
        }
        public async Task<IEnumerable<RelSolPractBhEstadoProgramaModel>> GetRelSolPractBhEstadoProgramaAsync()
        {
            _dbContext.Database.SetCommandTimeout(120);
            return await _dbContext.Bh_Estado_Programa.ToListAsync();
        }
        public async Task<IEnumerable<RelSolPractBhEstadoTurnoModel>> GetRelSolPractBhEstadoTurnoAsync()
        {
            _dbContext.Database.SetCommandTimeout(120);
            return await _dbContext.Bh_Estado_Turno.ToListAsync();
        }
        public async Task<IEnumerable<NnMotivoNoTurnoModel>> GetMotivoNoTurnoAsync()
        {
            _dbContext.Database.SetCommandTimeout(120);
            return await _dbContext.NN_MOTIVO_NO_TURNO.ToListAsync();
        }

        public async Task<IEnumerable<ObrasSocialesLaboModel>> GeOsLaboAsync()
        {
            return await _dbContext3.V_LABO_OS_MOSTRAR.ToListAsync();
        }
    }
}
