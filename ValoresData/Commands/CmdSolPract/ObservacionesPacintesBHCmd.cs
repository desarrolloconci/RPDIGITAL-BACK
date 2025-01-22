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
    public class ObservacionesPacintesBHCmd : IObservacionesPacientesBhCmd
    {
        private readonly DataBaseContext _dbcontext;
        public ObservacionesPacintesBHCmd(DataBaseContext dataBaseContext)
        {
            _dbcontext = dataBaseContext;
        }
        public async Task<bool> DeleteObservacionesPacientesBhAsync(int id)
        {
            var obs = await GetObservacionesPacientesBhADetailAsync(id);
            if (obs is null)
            {
                return false;
            }
            _dbcontext.OBSERVACIONES_PACIENTES_BH.Remove(obs);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<ObservacionesPacientesBhModel> GetObservacionesPacientesBhADetailAsync(int id)
        {
            return await _dbcontext.OBSERVACIONES_PACIENTES_BH.FindAsync(id); ;
        }

        public async Task<IEnumerable<ObservacionesPacientesBhModel>> GetObservacionesPacientesBhAsync()
        {
            return await _dbcontext.OBSERVACIONES_PACIENTES_BH.ToListAsync();
        }

        public async Task<bool> InsertObservacionesPacientesBhsync(ObservacionesPacientesBhModel Observacion)
        {
            if (Observacion == null)
            {
                return false;
            }
            _dbcontext.OBSERVACIONES_PACIENTES_BH.Add(Observacion);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatObservacionesPacientesBhAsync(ObservacionesPacientesBhModel Observacion)
        {
            var obs = await GetObservacionesPacientesBhAByDniDetailAsync(Observacion.dni);
            if (obs != null)
            {
                obs.Observacion = Observacion.Observacion;
                obs.dni= Observacion.dni;
                obs.usuario = Observacion.usuario;
                obs.creado = Observacion.creado;
                await _dbcontext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<ObservacionesPacientesBhModel> GetObservacionesPacientesBhAByDniDetailAsync(string dni)
        {
            return await _dbcontext.OBSERVACIONES_PACIENTES_BH.FirstOrDefaultAsync(e => e.dni == dni);
        }

    }
}
