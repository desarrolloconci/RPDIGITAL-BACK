using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos;
using ValorModels.Models;

namespace ValoresData.Commands.CmdValor
{
    public class PracticasCmd : IPracticasCmd
    {
        private readonly DataBaseContext _context;
        public PracticasCmd(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<bool> DeletePracticaAsync(int id)
        {
            var dbprograma = await GetPracticaAsyncById(id);
            if (dbprograma is null)
            {
                return false;
            }
            _context.Practicas.Remove(dbprograma);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PracticasModel> GetPracticaAsyncById(int id)
        {
            return await _context.Practicas.FindAsync(id);

        }

        public async Task<IEnumerable<PracticasModel>> GetPracticasAsync()
        {
            return await _context.Practicas.ToListAsync();
        }

        public async Task<bool> InsertPracticaAsync(PracticasModel Practica)
        {
            _context.Practicas.Add(Practica);
            await _context.SaveChangesAsync();
            if (Practica != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdatePracticaAsync(PracticasModel Practica)
        {
            var dbPrograma = await _context.Practicas.FindAsync(Practica.Id);
            if (dbPrograma != null)
            {
                dbPrograma.Id_Programa = Practica.Id_Programa;
                dbPrograma.Cod_practica = Practica.Cod_practica;
                dbPrograma.Nombre_Practica = Practica.Nombre_Practica;
                dbPrograma.Opcional = Practica.Opcional;
                dbPrograma.Activo = Practica.Activo;
                dbPrograma.Creado = Practica.Creado;
                dbPrograma.Usuario = Practica.Usuario;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<PracticasDto>> GetPracticasDtoAsync()
        {
            var result = await ( from v in _context.Practicas 
                                
                                join e in _context.programas on v.Id_Programa equals e.id 
                                select new PracticasDto
                                {
                                    Id = v.Id,
                                    Id_Programa=v.Id_Programa,
                                    Cod_practica= v.Cod_practica,
                                    Nombre_Practica= v.Nombre_Practica,
                                    Opcional= v.Opcional,
                                    Activo = v.Activo,
                                    Creado= v.Creado,
                                    Usuario= v.Usuario,
                                    Nombre_programa=e.Nombre_programa
                                } ).ToListAsync();

            return result;
        }
    }
}
