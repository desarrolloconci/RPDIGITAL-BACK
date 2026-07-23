using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using ValorModels.Dtos;


namespace ValoresData.Commands.CmdValor
{
    public class ListadoTurnoCmd : IListadoTurnoCmd
    {
        private readonly DataBase2Context _dbContext;
        private readonly DataBaseContext _context;
        public ListadoTurnoCmd(DataBase2Context dbContext, DataBaseContext context)
        {
            _dbContext = dbContext;
            _context = context;
        }

        public async Task<IEnumerable<ListadoTurnosModel>> GetListadoTurnoAsync()
        {
            return await _dbContext.vListadoTurnos.Take(100).ToListAsync();
        }

        public async Task<IEnumerable<ListadoTurnosModel>> GetListadoTurnoByDni(string dni, DateOnly fecha, string? idEstudio = null, string? metodo = null)
        {
            string dnisinceros = dni.TrimStart('0');
            string dniFormateado = dnisinceros.PadLeft(9, '0');

            var fechaDateTime = fecha.ToDateTime(TimeOnly.MinValue);
            var result = await _dbContext.vListadoTurnos.Where(e => e.fic_nrodoc == dniFormateado && e.tur_fecha.Value >= fechaDateTime && e.cancelado == "NO").ToListAsync();

            await CompletarPracticaAsync(result);

            if (!string.IsNullOrEmpty(idEstudio) || !string.IsNullOrEmpty(metodo))
            {
                await CompletarRecomendadoAsync(result, idEstudio, metodo);
            }

            return result;
            //&& !_context.REL_SOL_PRACT.Any(o => o.turno_id == e.turno_id)
            //var fechaInicio = fecha.ToDateTime(TimeOnly.MinValue);
            //var fechaFin = fecha.ToDateTime(TimeOnly.MaxValue);

            //var result = await _dbContext.vListadoTurnos
            //    .Where(e => e.fic_nrodoc == dni
            //                && e.tur_fecha >= fechaInicio
            //                && e.tur_fecha <= fechaFin
            //                && e.cancelado == "NO")
            //    .ToListAsync();
            //return result;
        }

        // Para cada turno, busca si tiene estudio(s) asociado en AG_TURNO_ESTUDIO y, de ser así,
        // resuelve código y nombre de práctica en el nomenclador correspondiente (servidor enlazado
        // SRV-DESA01, TWCC.dbo.V_MT_NOMENCLADOR), filtrando por el mismo tipoNomenclador_id que ya
        // trae el turno. Si un turno no tiene estudio en AG_TURNO_ESTUDIO, o si no hay un cruce con
        // el nomenclador del turno, se deja Practica en null: el front usa el fallback nom_cod/nom_nom
        // que ya viene del turnero. Si tiene varios estudios asociados, se concatenan.
        private async Task CompletarPracticaAsync(List<ListadoTurnosModel> turnos)
        {
            var turnoIds = turnos.Where(t => t.turno_id.HasValue && t.turno_id.Value != 0)
                .Select(t => t.turno_id!.Value).Distinct().ToList();
            if (!turnoIds.Any()) return;

            var estudiosPorTurno = await _context.AG_TURNO_ESTUDIO
                .Where(e => turnoIds.Contains(e.TURNO_ID))
                .ToListAsync();
            if (!estudiosPorTurno.Any()) return;

            var estudioIds = estudiosPorTurno.Select(e => e.ESTUDIO_ID).Distinct().ToList();
            var pEstudioIds = new SqlParameter("@estudioIds", JsonSerializer.Serialize(estudioIds));
            var sql = @"SELECT ESTUDIO_ID, NOMENCLADOR_ID, NOMBRE, PRACTICA_CODIGO
                        FROM [SRV-DESA01].[TWCC].[dbo].[V_MT_NOMENCLADOR]
                        WHERE ESTUDIO_ID IN (SELECT [value] FROM OPENJSON(@estudioIds) WITH ([value] int '$'))";
            var nomencladores = await _context.V_MT_NOMENCLADOR.FromSqlRaw(sql, pEstudioIds).ToListAsync();

            var estudiosPorTurnoLookup = estudiosPorTurno.ToLookup(e => e.TURNO_ID);

            foreach (var turno in turnos)
            {
                if (!turno.turno_id.HasValue) continue;

                var estudios = estudiosPorTurnoLookup[turno.turno_id.Value];
                if (!estudios.Any()) continue;

                var practicas = estudios
                    .Select(e =>
                        nomencladores.FirstOrDefault(n => n.ESTUDIO_ID == e.ESTUDIO_ID && n.NOMENCLADOR_ID == turno.tipoNomenclador_id)
                        ?? nomencladores.FirstOrDefault(n => n.ESTUDIO_ID == e.ESTUDIO_ID))
                    .Where(n => n != null)
                    .Select(n => $"{n!.NOMBRE?.Trim()} ({n.PRACTICA_CODIGO?.Trim()})")
                    .Distinct()
                    .ToList();

                if (practicas.Any())
                    turno.Practica = string.Join(", ", practicas);
            }
        }

        // Replica las 3 reglas de match de PP_BUSCAR_ATENCIONES_BH_3, pero contra el servicio_id
        // del turno (Geclisa) en vez del servicio_id de una atención de MULTICONSULTA (MIND) -
        // ambos comparten el mismo espacio de IDs (confirmado contra MIND.SERVICIOMEDICO).
        // Regla 1: metodo del pedido == metodo de la unidad correspondiente al servicio del turno.
        // Regla 2: idEstudio del pedido == estudio de la unidad "Check Up" (UNIDAD_ID=3).
        // Regla 3: idEstudio del pedido == estudio asociado directamente al servicio del turno.
        // Un turno queda EsRecomendado=true si su servicio_id matchea cualquiera de las 3.
        private async Task CompletarRecomendadoAsync(List<ListadoTurnosModel> turnos, string? idEstudio, string? metodo)
        {
            var servicioIds = turnos.Select(t => t.servicio_id).Distinct().ToList();
            if (!servicioIds.Any()) return;

            var pServicioIds = new SqlParameter("@servicioIds", JsonSerializer.Serialize(servicioIds));
            var pIdEstudio = new SqlParameter("@idEstudio", (object?)idEstudio ?? DBNull.Value);
            var pMetodo = new SqlParameter("@metodo", (object?)metodo ?? DBNull.Value);

            var sql = @"
                SELECT DISTINCT vu.SERVICIO_ID
                FROM [SRV-DESA02].MIND.dbo.v_unidadmedicadet vu
                INNER JOIN [SRV-DESA02].ETL.dbo.V_BH_METODOS_UNIDADES mu ON vu.UNIDAD_ID = mu.UNIDAD_ID
                WHERE vu.SERVICIO_ID IN (SELECT [value] FROM OPENJSON(@servicioIds) WITH ([value] int '$'))
                  AND @metodo IS NOT NULL AND LTRIM(RTRIM(mu.METODO_NOMBRE)) = LTRIM(RTRIM(@metodo))

                UNION

                SELECT DISTINCT vu.SERVICIO_ID
                FROM [SRV-DESA02].MIND.dbo.v_unidadmedicadet vu
                INNER JOIN [SRV-DESA02].ETL.dbo.V_BH_ESTUDIOS_UNIDADES eu ON vu.UNIDAD_ID = eu.UNIDAD_ID
                WHERE vu.SERVICIO_ID IN (SELECT [value] FROM OPENJSON(@servicioIds) WITH ([value] int '$'))
                  AND vu.UNIDAD_ID = 3
                  AND @idEstudio IS NOT NULL AND TRY_CAST(eu.ESTUDIO_CODIGO AS INT) = TRY_CAST(@idEstudio AS INT)

                UNION

                SELECT DISTINCT es.SERVICIO_ID
                FROM [SRV-DESA02].ETL.dbo.V_BH_ESTUDIOS_SERVICIOS es
                WHERE es.SERVICIO_ID IN (SELECT [value] FROM OPENJSON(@servicioIds) WITH ([value] int '$'))
                  AND @idEstudio IS NOT NULL AND TRY_CAST(es.ESTUDIO_CODIGO AS INT) = TRY_CAST(@idEstudio AS INT)";

            var recomendados = await _context.RecomendacionTurno.FromSqlRaw(sql, pServicioIds, pIdEstudio, pMetodo).ToListAsync();
            var servicioIdsRecomendados = recomendados.Select(r => r.SERVICIO_ID).ToHashSet();

            foreach (var turno in turnos)
            {
                turno.EsRecomendado = servicioIdsRecomendados.Contains(turno.servicio_id);
            }
        }

        public async Task<IEnumerable<ListadoServicioTurnoDto>> GetListadoserviciosByDni(string dni, DateOnly fecha)
        {
            var fechaDateTime = fecha.ToDateTime(TimeOnly.MinValue);
            string dnisinceros = dni.TrimStart('0');
            string dniFormateado = dnisinceros.PadLeft(9, '0');
            // var result = await _dbContext.vListadoTurnos.Where(e => e.fic_nrodoc == dni && e.tur_fecha.Value >= fechaDateTime && e.cancelado == "NO").GroupBy(e=>e.Serv_nombre).ToListAsync();
            var result = await _dbContext.vListadoTurnos
          .Where(e => e.fic_nrodoc == dniFormateado &&
                      e.tur_fecha >= fechaDateTime &&
                      e.cancelado == "NO")
          .GroupBy(e => e.Serv_nombre) 
          .Select(g => new ListadoServicioTurnoDto()
          {
              id= g.FirstOrDefault().servicio_id,
              Servicio=g.Key.Trim(),
          })     
          .ToListAsync();

            return result;
        }

    }
}