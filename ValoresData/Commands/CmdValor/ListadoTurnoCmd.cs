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

        public async Task<IEnumerable<ListadoTurnosModel>> GetListadoTurnoByDni(string dni, DateOnly fecha)
        {
            string dnisinceros = dni.TrimStart('0');
            string dniFormateado = dnisinceros.PadLeft(9, '0');

            var fechaDateTime = fecha.ToDateTime(TimeOnly.MinValue);
            var result = await _dbContext.vListadoTurnos.Where(e => e.fic_nrodoc == dniFormateado && e.tur_fecha.Value >= fechaDateTime && e.cancelado == "NO").ToListAsync();

            await CompletarPracticaAsync(result);

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