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
    public class BhEstudiosCmd:IBhEstudiosCmd
    { private readonly DataBaseContext _dbContext;
        private readonly PchimContext _pchimContext;
        public BhEstudiosCmd(DataBaseContext dbContext, PchimContext pchimContext)
        {
            _dbContext = dbContext;
            _pchimContext = pchimContext;
        }

        public async Task<IEnumerable<BhEstudiosModel>> GetBhEstudiosAsync(string? search)
        {
            var query = _dbContext.V_BH_ESTUDIOS.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e => e.ESTUDIO_NOMBRE.Contains(search) || e.ESTUDIO_CODIGO.Contains(search));
            }

            var estudios = await query.OrderBy(e => e.ESTUDIO_NOMBRE).ToListAsync();

            // Campos nuevos: resueltos contra PCHIM.ESTUDIOS_MASTER (SQL-02) en paralelo
            // a los viejos, para validar antes de reemplazarlos (ver plan 1.2/1.8).
            var nuevosPorCodigo = await _pchimContext.VEstudios
                .AsNoTracking()
                .Where(e => e.ESTUDIO_CODIGO != null && e.DISPONIBLE_RP == true)
                .ToDictionaryAsync(e => e.ESTUDIO_CODIGO!);

            foreach (var estudio in estudios)
            {
                if (estudio.ESTUDIO_CODIGO != null && nuevosPorCodigo.TryGetValue(estudio.ESTUDIO_CODIGO, out var nuevo))
                {
                    estudio.ESTUDIO_ID_NUEVO = nuevo.ESTUDIO_ID;
                    estudio.ESTUDIO_NOMBRE_NUEVO = nuevo.NOMBRE;
                    estudio.METODO_ID_NUEVO = nuevo.METODO_ID;
                }
            }

            // Solo se devuelven los estudios con DISPONIBLE_RP=1 en el catálogo nuevo (ver 1.8):
            // así el front puede armar el pedido con ESTUDIO_ID_NUEVO sin encontrarse nulls.
            return estudios.Where(e => e.ESTUDIO_ID_NUEVO != null);
        }
    }
}
