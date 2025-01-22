using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var fechaDateTime = fecha.ToDateTime(TimeOnly.MinValue);
            var result = await _dbContext.vListadoTurnos.Where(e => e.fic_nrodoc == dni && e.tur_fecha.Value >= fechaDateTime && e.cancelado == "NO").ToListAsync();
            return result;
            //&& !_context.REL_SOL_PRACT.Any(o => o.turno_id == e.turno_id)
        }

        public async Task<IEnumerable<ListadoServicioTurnoDto>> GetListadoserviciosByDni(string dni, DateOnly fecha)
        {
            var fechaDateTime = fecha.ToDateTime(TimeOnly.MinValue);
            // var result = await _dbContext.vListadoTurnos.Where(e => e.fic_nrodoc == dni && e.tur_fecha.Value >= fechaDateTime && e.cancelado == "NO").GroupBy(e=>e.Serv_nombre).ToListAsync();
            var result = await _dbContext.vListadoTurnos
          .Where(e => e.fic_nrodoc == dni &&
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