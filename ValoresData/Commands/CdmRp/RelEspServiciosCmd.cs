using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CdmRp
{
    public class RelEspServiciosCmd : IRelEspServiciosCmd
    {
        private readonly DataBaseContext _context;
        
        public RelEspServiciosCmd(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RelEspServiciosModel>> GetRelEspServiciosAsync()
        {
            return await _context.V_Rel_esp_servicios.ToListAsync();
        }
        public async Task<IEnumerable<RelSolPractBhServicioSolModel>> GetRelSolPractBhServicioSolAsync(int usuario_id)
        {
            var servicios = await GetRelEspServiciosDetailsAsync(usuario_id); 
            if (servicios != null && servicios.Any()) 
            {                
                var resultado = servicios.Select(s => new RelSolPractBhServicioSolModel
                {
                    IDSERVICIOSOLICITUD = s.servicio_id.ToString(),
                    SERVICIOSOLICITUD = s.SERVICIOSOLICITUD,
                });
                return resultado;
            }            
            return await _context.V_SolPractBhEspecialidad.ToListAsync();
        }

        public async Task<List<RelEspServiciosModel>> GetRelEspServiciosDetailsAsync(int usuario_id)
        {
            return await _context.V_Rel_esp_servicios.Where(e=> e.usuario_id == usuario_id).ToListAsync();
        }

        public async Task<bool> InsertRelEspServicios(RelEspServicioModel model)
        {
            if (model == null) return false;

            try
            {
                _context.REL_ESP_SERVICIOS.Add(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar: {ex.Message}");
                return false;
            }
        }

        public async Task<RelEspServicioModel> GetRelEspServiciosByIdAsync(int id)
        {
            return await _context.REL_ESP_SERVICIOS.FindAsync(id);
        }

        public async Task<bool> DeleteRelEspServiciosAsync(int id)
        {
            var dbServicio = await GetRelEspServiciosByIdAsync(id);
            if (dbServicio is null)
            {
                return false;
            }
            _context.REL_ESP_SERVICIOS.Remove(dbServicio);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
