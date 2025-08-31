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


    
    }
}
