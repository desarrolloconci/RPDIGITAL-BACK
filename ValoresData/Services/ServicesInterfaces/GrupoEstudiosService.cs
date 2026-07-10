using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Dtos;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.ServicesInterfaces
{
    public class GrupoEstudiosService : IGrupoEstudiosService
    {
        private readonly IGrupoEstudiosCmd _grupoEstudiosCmd;
        public GrupoEstudiosService(IGrupoEstudiosCmd  grupoEstudiosCmd)
        {
            _grupoEstudiosCmd = grupoEstudiosCmd;
        }
        public async Task<IEnumerable<GrupoEstudiosModel>> GetGrupoEstudiosAsync()
        {
            return await _grupoEstudiosCmd.GetGrupoEstudiosAsync();
        }
        public async Task<GrupoEstudiosModel> CrearBateriaPersonalAsync(CrearBateriaPersonalDto dto)
        {
            return await _grupoEstudiosCmd.CrearBateriaPersonalAsync(dto);
        }
        public async Task<ResultadoOperacionBateria> EditarBateriaPersonalAsync(int grupoId, CrearBateriaPersonalDto dto)
        {
            return await _grupoEstudiosCmd.EditarBateriaPersonalAsync(grupoId, dto);
        }
        public async Task<ResultadoOperacionBateria> EliminarBateriaPersonalAsync(int grupoId, int usuarioId)
        {
            return await _grupoEstudiosCmd.EliminarBateriaPersonalAsync(grupoId, usuarioId);
        }
        public async Task<GrupoEstudiosModel> CrearBateriaGeneralAsync(CrearBateriaGeneralDto dto)
        {
            return await _grupoEstudiosCmd.CrearBateriaGeneralAsync(dto);
        }
        public async Task<ResultadoOperacionBateria> EditarBateriaGeneralAsync(int grupoId, CrearBateriaGeneralDto dto)
        {
            return await _grupoEstudiosCmd.EditarBateriaGeneralAsync(grupoId, dto);
        }
        public async Task<ResultadoOperacionBateria> EliminarBateriaGeneralAsync(int grupoId)
        {
            return await _grupoEstudiosCmd.EliminarBateriaGeneralAsync(grupoId);
        }
        public async Task<IEnumerable<int>> GetUsuariosAsignadosAsync(int grupoId)
        {
            return await _grupoEstudiosCmd.GetUsuariosAsignadosAsync(grupoId);
        }
        public async Task<IEnumerable<AsignacionBateriaDto>> GetTodasLasAsignacionesAsync()
        {
            return await _grupoEstudiosCmd.GetTodasLasAsignacionesAsync();
        }
    }
}
