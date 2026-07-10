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
    public class RelEspBateriaCmd : IRelEspBateriaCmd
    {
        private readonly DataBaseContext _context;
        public RelEspBateriaCmd(DataBaseContext dataBaseContext)
        {
            _context = dataBaseContext;
        }
        public async Task<IEnumerable<RelEspBateriasModel>> GetRelEspServiciosAsync()
        {
            return await _context.V_Rel_esp_baterias.ToListAsync();
        }

        public async Task<IEnumerable<BhBateriasModel>> GetRelSolPractBhBateriaAsync(int usuario_id)
        {
            var creadasPorUsuario = await _context.GRUPOESTUDIOS_CREADOR
                .Where(c => c.USUARIO_ID == usuario_id)
                .Select(c => c.GRUPO_ID)
                .ToListAsync();

            List<BhBateriasModel> resultadoFinal;

            var baterias = await GetRelEspServiciosByUserAsync(usuario_id);
            if (baterias != null && baterias.Any())
            {
                var resultado = baterias.Select(s => new BhBateriasModel
                {
                    id = s.bateria_id,
                    nombre = s.nombre,
                    esPropia = creadasPorUsuario.Contains(s.bateria_id),
                });
                var publicas = await BateriaPublicamodel();
                var resultadoPublicas = publicas.Select(p => new BhBateriasModel
                {
                    id = p.id,
                    nombre = p.nombre,
                    esPropia = creadasPorUsuario.Contains(p.id),
                });
                resultadoFinal = resultado.Concat(resultadoPublicas).ToList();
            }
            else
            {
                var todas = await _context.v_BH_BATERIAS
                    .Select(b => new BhBateriasModel { id = b.id, nombre = b.nombre })
                    .ToListAsync();
                foreach (var bateria in todas)
                {
                    bateria.esPropia = creadasPorUsuario.Contains(bateria.id);
                }
                resultadoFinal = todas;
            }

            var ids = resultadoFinal.Select(b => b.id).Distinct().ToList();
            var descripciones = await _context.GRUPOESTUDIOS
                .Where(g => ids.Contains(g.id))
                .ToDictionaryAsync(g => g.id, g => g.Descripcion);

            foreach (var bateria in resultadoFinal)
            {
                bateria.descripcion = descripciones.TryGetValue(bateria.id, out var desc) ? desc : string.Empty;
            }

            return resultadoFinal;
        }
        public async Task<IEnumerable<RelEspBateriasModel>> GetRelEspServiciosByUserAsync(int usuario_id)
        {
            return await _context.V_Rel_esp_baterias.Where(e => e.usuario_id == usuario_id).ToListAsync();
        }

        public async Task<IEnumerable<BhBateriasPublicasModel>> BateriaPublicamodel()
        {
            return await _context.V_BATERIAS_UNIVERSALES.ToListAsync();
        }

        public async Task<bool> InsertRelEspBaterias(RelBateriasEspModel model)
        {
            if (model == null) return false;

            try
            {
                _context.Rel_esp_baterias.Add(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error al insertar: {ex.Message}");
                return false;
            }
        }
        public async Task<RelBateriasEspModel> GetRelEspServiciosByidAsync(int id)
        {
            return await _context.Rel_esp_baterias.FindAsync(id);
        }
        public async Task<bool> DeleteRelEspBateriasAsync(int id)
        {
            var dbExcepcion = await GetRelEspServiciosByidAsync(id);
            if (dbExcepcion is null)
            {
                return false;
            }
            _context.Rel_esp_baterias.Remove(dbExcepcion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
