using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdSolPract
{
    public class AsignacionEstadoProgramaCmd : IAsignacionEstadoProgramaCmd
    {
        private readonly DataBaseContext _dbContext;
        public AsignacionEstadoProgramaCmd(DataBaseContext dbconext)
        {
            _dbContext = dbconext; 
        }

        public async Task<bool> DeleteAsignacionEstadoProgramaAsync(string dni, string unidad)
        {
            var Asig = await _dbContext.ASIGNACION_ESTADO_PROGRAMA.Where(e => e.dni == dni && e.unidad == unidad).FirstOrDefaultAsync();
            if (Asig is null)
            {
                return false;
            }
            _dbContext.ASIGNACION_ESTADO_PROGRAMA.Remove(Asig);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AsignacionEstadoProgramaModel>> GetAsignacionEstadoProgramaAsync()
        {
            return await _dbContext.ASIGNACION_ESTADO_PROGRAMA.ToListAsync();
        }

        public async Task<AsignacionEstadoProgramaModel> GetAsignacionEstadoProgramaDetailAsync(string dni, string unidad)
        {
            return await _dbContext.ASIGNACION_ESTADO_PROGRAMA.Where(e => e.dni == dni && e.unidad == unidad).FirstOrDefaultAsync();
        }

        public async Task<bool> InsertAsignacionProgramaAsync(AsignacionEstadoProgramaModel asignacion)
        {
            if (asignacion == null) return false;

            try
            {
                _dbContext.ASIGNACION_ESTADO_PROGRAMA.Add(asignacion);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Registrar el error
                Console.WriteLine($"Error al insertar: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdatAsignacionEstadoProgramaAsync(AsignacionEstadoProgramaModel asignacion)
        {
            var dbasignacion = await _dbContext.ASIGNACION_ESTADO_PROGRAMA
             .Where(e => e.dni == asignacion.dni && e.unidad == asignacion.unidad)
              .FirstOrDefaultAsync();
            if (dbasignacion != null)
            {
                dbasignacion.dni = asignacion.dni;
                dbasignacion.unidad = asignacion.unidad;
                dbasignacion.estado_id = asignacion.estado_id;
                dbasignacion.usuario = asignacion.usuario;
                dbasignacion.creado = asignacion.creado;

                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
