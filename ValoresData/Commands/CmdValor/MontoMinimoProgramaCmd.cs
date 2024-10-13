using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos;
using ValorModels.Dtos.LoginDto;
using ValorModels.Models;

namespace ValoresData.Commands.CmdValor
{
    public class MontoMinimoProgramaCmd : IMontoMinimoProgramaCmd
    {
        private readonly DataBaseContext _context;

        public MontoMinimoProgramaCmd(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteMontoAsync(int id)
        {
            var dbMonto = await GetMontoDetailsByid(id);
            if (dbMonto is null)
            {
                return false;
            }
            _context.Importe_Minimo_Programa.Remove(dbMonto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MontoMinimoDto>> GetMontoAsync()
        {
            var result = await (from v in _context.Importe_Minimo_Programa
                                join e in _context.programas on v.id_programa equals e.id
                                select new MontoMinimoDto
                                {
                                    id = v.id,
                                    id_programa = v.id_programa,
                                    Nombre_programa = e.Nombre_programa,
                                    importe_minimo = v.importe_minimo,
                                    importe_minimo_apross = v.importe_minimo_apross,
                                    fecha = v.fecha,
                                    usuario_creacion = v.usuario_creacion,
                                    creado = v.creado
                                }).ToListAsync();
                               
          return result;
        }
        public async Task<MontoMinimoProgramaModel> GetMontoMesAñoAsyn(int id, int mes, int año)
        {
            var result = await _context.Importe_Minimo_Programa.Where(e => e.id_programa == id && e.fecha.Month == mes && e.fecha.Year == año)
                      .Select(e => new MontoMinimoProgramaModel
                      {
                          id = e.id,
                          id_programa = e.id_programa,
                          importe_minimo = e.importe_minimo,
                          importe_minimo_apross = e.importe_minimo_apross,
                          fecha = e.fecha,
                          usuario_creacion = e.usuario_creacion,
                          creado = e.creado
                      }).FirstOrDefaultAsync();

            return result;

        }
        public async Task<MontoMinimoProgramaModel> GetMontoDetailsByid(int id)
        {
            var result = await _context.Importe_Minimo_Programa.Where(e => e.id == id)
                    .Select(e => new MontoMinimoProgramaModel
                    {
                        id = e.id,
                        id_programa = e.id_programa,
                        importe_minimo = e.importe_minimo,
                        importe_minimo_apross = e.importe_minimo_apross,
                        fecha = e.fecha,
                        usuario_creacion = e.usuario_creacion,
                        creado = e.creado
                    }).FirstOrDefaultAsync();


            return result;

        }
        public async Task<MontoMinimoProgramaModel> GetMontoDetails(int id)
        {
            var result = await _context.Importe_Minimo_Programa.Where(e => e.id_programa == id)
                      .Select(e => new MontoMinimoProgramaModel
                      {
                          id = e.id,
                          id_programa = e.id_programa,
                          importe_minimo=e.importe_minimo,
                          importe_minimo_apross=e.importe_minimo_apross,
                          fecha=e.fecha,
                          usuario_creacion=e.usuario_creacion,
                          creado=e.creado
                      }).FirstOrDefaultAsync();


            return result;
        }

        public async Task<bool> InsertMontoAsync(MontoMinimoProgramaModel Monto)
        {
            _context.Importe_Minimo_Programa.Add(Monto);
            await _context.SaveChangesAsync();
            if (Monto != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateMontoAsync(MontoMinimoProgramaModel Monto)
        {
            var dbMonto = await _context.Importe_Minimo_Programa.FindAsync(Monto.id);
            if (dbMonto != null)
            {
                dbMonto.id_programa= Monto.id_programa;
                dbMonto.importe_minimo= Monto.importe_minimo;
                dbMonto.importe_minimo_apross=Monto.importe_minimo_apross;
                dbMonto.fecha = Monto.fecha;
                dbMonto.usuario_creacion = Monto.usuario_creacion;
                dbMonto.creado = Monto.creado;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}
