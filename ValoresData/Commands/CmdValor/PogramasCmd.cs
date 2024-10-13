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
    public class PogramasCmd : IProgramaCmd
    {
        private readonly DataBaseContext _context;
        public PogramasCmd(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteProgramaAsync(int id)
        {
            var dbprograma = await GetProgramaDetailAsync(id);
            if (dbprograma is null)
            {
                return false;
            }_context.programas.Remove(dbprograma);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProgramasModel>> GetProgramaAsync()
        {
            return await _context.programas.ToListAsync();
        }

        public async Task<ProgramasModel> GetProgramaDetailAsync(int id)
        {
            return await _context.programas.FindAsync(id);
        }

        public async Task<bool> InsertProgramasync(ProgramasModel programaAtencion)
        {
            _context.programas.Add(programaAtencion);
            await _context.SaveChangesAsync();
            if (programaAtencion != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateProgramaAsync(ProgramasModel programaAtencion)
        {
            var dbPrograma = await _context.programas.FindAsync(programaAtencion.id);
            if (dbPrograma != null)
            {
                dbPrograma.Nombre_programa = programaAtencion.Nombre_programa;
                dbPrograma.activo = programaAtencion.activo;
                dbPrograma.creado = programaAtencion.creado;
                dbPrograma.usuario = programaAtencion.usuario;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }
    }
}
