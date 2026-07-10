using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CmdSolPract
{
    public class GrupoEstudiosCmd : IGrupoEstudiosCmd
    {
        private readonly DataBaseContext _context;
        public GrupoEstudiosCmd(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<GrupoEstudiosModel>> GetGrupoEstudiosAsync()
        {
            var grupos = await _context.GRUPOESTUDIOS.ToListAsync();

            var creadores = await _context.GRUPOESTUDIOS_CREADOR
                .ToDictionaryAsync(c => c.GRUPO_ID, c => c.USUARIO_ID);

            foreach (var grupo in grupos)
            {
                grupo.CreadoPorUsuarioId = creadores.TryGetValue(grupo.id, out var usuarioId) ? usuarioId : null;
            }

            return grupos;
        }

        public async Task<GrupoEstudiosModel> CrearBateriaPersonalAsync(CrearBateriaPersonalDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var grupo = new GrupoEstudiosModel
                {
                    Codigo = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(),
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion ?? string.Empty,
                    Habilitada = 1,
                    Publica = 0,
                    Baja = false
                };
                _context.GRUPOESTUDIOS.Add(grupo);
                await _context.SaveChangesAsync();

                var detalles = dto.EstudioIds.Select(estudioId => new GrupoEstudiosDetModel
                {
                    GRUPO_ID = grupo.id,
                    ESTUDIO_ID = estudioId,
                    BAJA = false,
                    CREADO = DateTime.Now
                });
                _context.GRUPOESTUDIOSDET.AddRange(detalles);

                _context.Rel_esp_baterias.Add(new RelBateriasEspModel
                {
                    usuario_id = dto.UsuarioId,
                    bateria_id = grupo.id
                });

                _context.GRUPOESTUDIOS_CREADOR.Add(new GrupoEstudiosCreadorModel
                {
                    GRUPO_ID = grupo.id,
                    USUARIO_ID = dto.UsuarioId,
                    CREADO = DateTime.Now
                });

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return grupo;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ResultadoOperacionBateria> EditarBateriaPersonalAsync(int grupoId, CrearBateriaPersonalDto dto)
        {
            var creador = await _context.GRUPOESTUDIOS_CREADOR.FirstOrDefaultAsync(c => c.GRUPO_ID == grupoId);
            if (creador == null)
            {
                return ResultadoOperacionBateria.NoEncontrada;
            }
            if (creador.USUARIO_ID != dto.UsuarioId)
            {
                return ResultadoOperacionBateria.NoAutorizado;
            }

            var grupo = await _context.GRUPOESTUDIOS.FirstOrDefaultAsync(g => g.id == grupoId);
            if (grupo == null)
            {
                return ResultadoOperacionBateria.NoEncontrada;
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                grupo.Nombre = dto.Nombre;
                grupo.Descripcion = dto.Descripcion ?? string.Empty;

                var detallesViejos = await _context.GRUPOESTUDIOSDET.Where(d => d.GRUPO_ID == grupoId).ToListAsync();
                _context.GRUPOESTUDIOSDET.RemoveRange(detallesViejos);

                var detallesNuevos = dto.EstudioIds.Select(estudioId => new GrupoEstudiosDetModel
                {
                    GRUPO_ID = grupoId,
                    ESTUDIO_ID = estudioId,
                    BAJA = false,
                    CREADO = DateTime.Now
                });
                _context.GRUPOESTUDIOSDET.AddRange(detallesNuevos);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return ResultadoOperacionBateria.Ok;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ResultadoOperacionBateria> EliminarBateriaPersonalAsync(int grupoId, int usuarioId)
        {
            var creador = await _context.GRUPOESTUDIOS_CREADOR.FirstOrDefaultAsync(c => c.GRUPO_ID == grupoId);
            if (creador == null)
            {
                return ResultadoOperacionBateria.NoEncontrada;
            }
            if (creador.USUARIO_ID != usuarioId)
            {
                return ResultadoOperacionBateria.NoAutorizado;
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var detalles = await _context.GRUPOESTUDIOSDET.Where(d => d.GRUPO_ID == grupoId).ToListAsync();
                _context.GRUPOESTUDIOSDET.RemoveRange(detalles);

                var relaciones = await _context.Rel_esp_baterias.Where(r => r.bateria_id == grupoId).ToListAsync();
                _context.Rel_esp_baterias.RemoveRange(relaciones);

                _context.GRUPOESTUDIOS_CREADOR.Remove(creador);

                var grupo = await _context.GRUPOESTUDIOS.FirstOrDefaultAsync(g => g.id == grupoId);
                if (grupo != null)
                {
                    _context.GRUPOESTUDIOS.Remove(grupo);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return ResultadoOperacionBateria.Ok;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<GrupoEstudiosModel> CrearBateriaGeneralAsync(CrearBateriaGeneralDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var grupo = new GrupoEstudiosModel
                {
                    Codigo = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(),
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion ?? string.Empty,
                    Habilitada = 1,
                    Publica = dto.Publica ? 1 : 0,
                    Baja = false
                };
                _context.GRUPOESTUDIOS.Add(grupo);
                await _context.SaveChangesAsync();

                var detalles = dto.EstudioIds.Select(estudioId => new GrupoEstudiosDetModel
                {
                    GRUPO_ID = grupo.id,
                    ESTUDIO_ID = estudioId,
                    BAJA = false,
                    CREADO = DateTime.Now
                });
                _context.GRUPOESTUDIOSDET.AddRange(detalles);

                var asignaciones = (dto.UsuarioIds ?? new List<int>()).Select(usuarioId => new RelBateriasEspModel
                {
                    usuario_id = usuarioId,
                    bateria_id = grupo.id
                });
                _context.Rel_esp_baterias.AddRange(asignaciones);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return grupo;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ResultadoOperacionBateria> EditarBateriaGeneralAsync(int grupoId, CrearBateriaGeneralDto dto)
        {
            var grupo = await _context.GRUPOESTUDIOS.FirstOrDefaultAsync(g => g.id == grupoId);
            if (grupo == null)
            {
                return ResultadoOperacionBateria.NoEncontrada;
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                grupo.Nombre = dto.Nombre;
                grupo.Descripcion = dto.Descripcion ?? string.Empty;
                grupo.Publica = dto.Publica ? 1 : 0;

                var detallesViejos = await _context.GRUPOESTUDIOSDET.Where(d => d.GRUPO_ID == grupoId).ToListAsync();
                _context.GRUPOESTUDIOSDET.RemoveRange(detallesViejos);

                var detallesNuevos = dto.EstudioIds.Select(estudioId => new GrupoEstudiosDetModel
                {
                    GRUPO_ID = grupoId,
                    ESTUDIO_ID = estudioId,
                    BAJA = false,
                    CREADO = DateTime.Now
                });
                _context.GRUPOESTUDIOSDET.AddRange(detallesNuevos);

                var asignacionesViejas = await _context.Rel_esp_baterias.Where(r => r.bateria_id == grupoId).ToListAsync();
                _context.Rel_esp_baterias.RemoveRange(asignacionesViejas);

                var asignacionesNuevas = (dto.UsuarioIds ?? new List<int>()).Select(usuarioId => new RelBateriasEspModel
                {
                    usuario_id = usuarioId,
                    bateria_id = grupoId
                });
                _context.Rel_esp_baterias.AddRange(asignacionesNuevas);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return ResultadoOperacionBateria.Ok;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ResultadoOperacionBateria> EliminarBateriaGeneralAsync(int grupoId)
        {
            var grupo = await _context.GRUPOESTUDIOS.FirstOrDefaultAsync(g => g.id == grupoId);
            if (grupo == null)
            {
                return ResultadoOperacionBateria.NoEncontrada;
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var detalles = await _context.GRUPOESTUDIOSDET.Where(d => d.GRUPO_ID == grupoId).ToListAsync();
                _context.GRUPOESTUDIOSDET.RemoveRange(detalles);

                var asignaciones = await _context.Rel_esp_baterias.Where(r => r.bateria_id == grupoId).ToListAsync();
                _context.Rel_esp_baterias.RemoveRange(asignaciones);

                var creador = await _context.GRUPOESTUDIOS_CREADOR.FirstOrDefaultAsync(c => c.GRUPO_ID == grupoId);
                if (creador != null)
                {
                    _context.GRUPOESTUDIOS_CREADOR.Remove(creador);
                }

                _context.GRUPOESTUDIOS.Remove(grupo);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return ResultadoOperacionBateria.Ok;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<int>> GetUsuariosAsignadosAsync(int grupoId)
        {
            return await _context.Rel_esp_baterias
                .Where(r => r.bateria_id == grupoId)
                .Select(r => r.usuario_id)
                .ToListAsync();
        }

        public async Task<IEnumerable<AsignacionBateriaDto>> GetTodasLasAsignacionesAsync()
        {
            return await _context.Rel_esp_baterias
                .Select(r => new AsignacionBateriaDto { BateriaId = r.bateria_id, UsuarioId = r.usuario_id })
                .ToListAsync();
        }
    }
}
