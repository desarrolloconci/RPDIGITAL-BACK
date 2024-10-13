using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos;
using ValorModels.Models;

namespace ValoresData.Commands.CmdValor
{
    public class ExcepcionesCmd: IExcepcionesCmd
    {
        private readonly DataBaseContext _context;

        public ExcepcionesCmd(DataBaseContext context)
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
            _context.excepciones.Remove(dbExcepcion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ExcepcionesDto>> GetExcepcionesAsync()
        {
            var result = await (from v in _context.excepciones
                                join e in _context.programas on v.ID_PROGRAMA equals e.id

                                select new ExcepcionesDto
                                {
                                    id = v.id,
                                    codigo_os = v.codigo_os,
                                    ID_PROGRAMA = v.ID_PROGRAMA,
                                    Programa = e.Nombre_programa,
                                    excepcion = v.excepcion,
                                    usuario_creacion = v.usuario_creacion,
                                    fecha = v.fecha,

                                }).ToListAsync();

            return result;
        }

        public async Task<ExcepcionesModel> GetExcepcionesDetailsAsync(int id)
        {
            var result = await _context.excepciones.Where(e => e.id == id)
                     .Select(e => new ExcepcionesModel
                     {
                         id = e.id,
                         codigo_os=e.codigo_os,
                         ID_PROGRAMA= e.ID_PROGRAMA,
                         excepcion = e.excepcion,  
                         fecha = e.fecha,
                         usuario_creacion = e.usuario_creacion
                     }).FirstOrDefaultAsync();


            return result;
        }
        public async Task<ExcepcionesModel> GetExcepcionesDetailsByOsAsync(int os)
        {
            var result = await _context.excepciones.Where(e => e.codigo_os == os)
                    .Select(e => new ExcepcionesModel
                    {
                        id = e.id,
                        codigo_os = e.codigo_os,
                        ID_PROGRAMA = e.ID_PROGRAMA,
                        excepcion = e.excepcion,
                        fecha = e.fecha,
                        usuario_creacion = e.usuario_creacion
                    }).FirstOrDefaultAsync();


            return result;
        }
        public async Task<ExcepcionesDto> GetExcepcionesDetailsByOsProgramaAsync(int os, int programa_id)
        { 
            var result = await (from v in _context.excepciones.Where(e => e.codigo_os == os && e.ID_PROGRAMA == programa_id)
                                join e in _context.programas on v.ID_PROGRAMA equals e.id
                               
                                select new ExcepcionesDto
                                {
                                    id = v.id,
                                    codigo_os = v.codigo_os, 
                                    ID_PROGRAMA = v.ID_PROGRAMA, 
                                    Programa = e.Nombre_programa,
                                    excepcion = v.excepcion,
                                    usuario_creacion = v.usuario_creacion,
                                    fecha= v.fecha,
                                   
                                }).FirstOrDefaultAsync();

            return result;
        }
        public async Task<bool> InsertExcepcionAsync(ExcepcionesModel excepciones)
        {
            _context.excepciones.Add(excepciones);
            await _context.SaveChangesAsync();
            if (excepciones != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateExcepcionAsync(ExcepcionesModel excepciones)
        {
            var dbExcepcion = await _context.excepciones.FindAsync(excepciones.id);
            if (dbExcepcion != null)
            {
                dbExcepcion.codigo_os = excepciones.codigo_os;
                dbExcepcion.ID_PROGRAMA = excepciones.ID_PROGRAMA;
                dbExcepcion.excepcion = excepciones.excepcion;
                dbExcepcion.fecha = excepciones.fecha;
                dbExcepcion.usuario_creacion = excepciones.usuario_creacion;
            

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
