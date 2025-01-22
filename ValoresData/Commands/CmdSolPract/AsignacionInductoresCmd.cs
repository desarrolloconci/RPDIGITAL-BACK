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
    public class AsignacionInductoresCmd : IAsignacionInductoresCmd
    {
        private readonly DataBaseContext _dbContext;
        public AsignacionInductoresCmd(DataBaseContext dataBase)
        {
            _dbContext = dataBase;
        }
        public async Task<bool> DeleteAsignacionInductoresAsync(string dni, string unidad)
        {
            var Asig = await _dbContext.ASIGNACION_INDUCTORES.Where(e=> e.DNI== dni && e.UNIDAD==unidad).FirstOrDefaultAsync();
            if (Asig is null)
            {
                return false;
            }
            _dbContext.ASIGNACION_INDUCTORES.Remove(Asig);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<AsignacionInductoresModel> GetAsignacionInductoresADetailAsync(string dni, string unidad)
        {
            return await _dbContext.ASIGNACION_INDUCTORES.Where(e => e.DNI == dni && e.UNIDAD == unidad).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AsignacionInductoresModel>> GetAsignacionInductoresAsync()
        {
            return await _dbContext.ASIGNACION_INDUCTORES.ToListAsync();    
        }

        public async Task<bool> InsertAsignacionInductoresAsync(AsignacionInductoresModel asignacion)
        {
            if (asignacion == null)
            {
                return false;
            }
            _dbContext.ASIGNACION_INDUCTORES.Add(asignacion);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatAsignacionInductoresAsync(AsignacionInductoresModel asignacion)
        {
            var dbasignacion = await GetAsignacionInductoresADetailAsync(asignacion.DNI,asignacion.UNIDAD);
            if (dbasignacion != null)
            {
                dbasignacion.DNI = asignacion.DNI;
                dbasignacion.UNIDAD = asignacion.UNIDAD;
                dbasignacion.ID_INDUCTOR= asignacion.ID_INDUCTOR;
                dbasignacion.usuario= asignacion.usuario;
                dbasignacion.CREADO = asignacion.CREADO;
                

                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
