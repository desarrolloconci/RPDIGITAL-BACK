using Microsoft.EntityFrameworkCore;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdSolPract
{
    public class RpBorradosCmd : IRpBorradosCmd
    {
        private readonly DataBaseContext _context;
        public RpBorradosCmd(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RpBorradoModel>> GetRpBorradosAsync(string? dni = null, string? startFecha = null, string? endFecha = null, string? usuarioBorro = null, string? prestador = null)
        {
            var query = _context.RP_BORRADOS.AsQueryable();

            if (!string.IsNullOrEmpty(dni))
                query = query.Where(e => e.DNI == dni);

            if (!string.IsNullOrEmpty(startFecha) && DateTime.TryParse(startFecha, out var start))
                query = query.Where(e => e.fechaBorrado >= start.Date);

            if (!string.IsNullOrEmpty(endFecha) && DateTime.TryParse(endFecha, out var end))
                query = query.Where(e => e.fechaBorrado <= end.Date.AddDays(1).AddTicks(-1));

            if (!string.IsNullOrEmpty(usuarioBorro))
                query = query.Where(e => e.usuarioBorro != null && e.usuarioBorro.Contains(usuarioBorro));

            if (!string.IsNullOrEmpty(prestador))
                query = query.Where(e => e.PRESTADORQUEGENERASOLICITUD != null && e.PRESTADORQUEGENERASOLICITUD.Contains(prestador));

            return await query.OrderByDescending(e => e.fechaBorrado).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetUsuariosBorradoAsync()
        {
            return await _context.RP_BORRADOS
                .Where(e => e.usuarioBorro != null && e.usuarioBorro != "")
                .Select(e => e.usuarioBorro!)
                .Distinct()
                .OrderBy(e => e)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetPrestadoresBorradoAsync()
        {
            return await _context.RP_BORRADOS
                .Where(e => e.PRESTADORQUEGENERASOLICITUD != null && e.PRESTADORQUEGENERASOLICITUD != "")
                .Select(e => e.PRESTADORQUEGENERASOLICITUD!.Trim())
                .Distinct()
                .OrderBy(e => e)
                .ToListAsync();
        }
    }
}
