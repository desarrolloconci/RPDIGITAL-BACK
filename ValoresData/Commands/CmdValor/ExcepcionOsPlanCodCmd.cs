using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models;

namespace ValoresData.Commands.CmdValor
{
    public class ExcepcionOsPlanCodCmd : IExcepcionOsPlanCodCmd
    {
        private readonly DataBaseContext _context;
        public ExcepcionOsPlanCodCmd(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteExcepcionAsync(int id)
        {
            var dbExcepcion = await GetExcepcionesDetailsAsync(id);
            if (dbExcepcion is null)
            {
                return false;
            }
            _context.Excepciones_os_plan_cod.Remove(dbExcepcion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ExcepcionOsPlanCodModel>> GetExcepcionesAsync()
        {
            return await _context.Excepciones_os_plan_cod.ToListAsync();
        }

        public async Task<ExcepcionOsPlanCodModel> GetExcepcionesDetailsOSPlanPracAsync(int os, int plan, string cod)
        {
            var result = await _context.Excepciones_os_plan_cod.Where(e => e.os_cod == os && e.plan_id == plan && e.Cod_practica == cod)
                  .Select(e => new ExcepcionOsPlanCodModel
                  {
                      id = e.id,
                      os_cod = e.os_cod,
                      plan_id = e.plan_id,
                      Cod_practica = e.Cod_practica,
                      Nombre_Practica = e.Nombre_Practica,
                      excepcion = e.excepcion,
                      Creado = e.Creado,
                      Usuario = e.Usuario
                  }).FirstOrDefaultAsync();


            return result;

        }
        public async Task<ExcepcionOsPlanCodModel> GetExcepcionesDetailsAsync(int id)
        {
            var result = await _context.Excepciones_os_plan_cod.Where(e => e.id == id)
                    .Select(e => new ExcepcionOsPlanCodModel
                    {
                        id = e.id,
                        os_cod = e.os_cod,
                        plan_id = e.plan_id,
                        Cod_practica = e.Cod_practica,
                        Nombre_Practica = e.Nombre_Practica,
                        excepcion = e.excepcion,
                        Creado = e.Creado,
                        Usuario = e.Usuario
                    }).FirstOrDefaultAsync();


            return result;
        }

        public async Task<ExcepcionOsPlanCodModel> GetExcepcionesDetailsByOsAsync(int os)
        {
            var result = await _context.Excepciones_os_plan_cod.Where(e => e.os_cod == os)
                    .Select(e => new ExcepcionOsPlanCodModel
                    {
                        id = e.id,
                        os_cod = e.os_cod,
                        plan_id = e.plan_id,
                        Cod_practica = e.Cod_practica,
                        Nombre_Practica = e.Nombre_Practica,
                        excepcion = e.excepcion,
                        Creado = e.Creado,
                        Usuario = e.Usuario
                    }).FirstOrDefaultAsync();


            return result;
        }

        public async Task<bool> InsertExcepcionAsync(ExcepcionOsPlanCodModel excepciones)
        {
            _context.Excepciones_os_plan_cod.Add(excepciones);
            await _context.SaveChangesAsync();
            if (excepciones != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateExcepcionAsync(ExcepcionOsPlanCodModel excepciones)
        {
            var dbExcepcion = await _context.Excepciones_os_plan_cod.FindAsync(excepciones.id);
            if (dbExcepcion != null)
            {
                dbExcepcion.os_cod = excepciones.os_cod;
                dbExcepcion.plan_id = excepciones.plan_id;
                dbExcepcion.Cod_practica= excepciones.Cod_practica;
                dbExcepcion.Nombre_Practica = excepciones.Nombre_Practica;
                dbExcepcion.excepcion =excepciones.excepcion;
                dbExcepcion.Creado = excepciones.Creado;
                dbExcepcion.Usuario= excepciones.Usuario;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
