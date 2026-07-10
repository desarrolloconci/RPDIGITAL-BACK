using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IGrupoEstudiosCmd
    {
        public Task<IEnumerable<GrupoEstudiosModel>> GetGrupoEstudiosAsync();
        public Task<GrupoEstudiosModel> CrearBateriaPersonalAsync(CrearBateriaPersonalDto dto);
        public Task<ResultadoOperacionBateria> EditarBateriaPersonalAsync(int grupoId, CrearBateriaPersonalDto dto);
        public Task<ResultadoOperacionBateria> EliminarBateriaPersonalAsync(int grupoId, int usuarioId);
        public Task<GrupoEstudiosModel> CrearBateriaGeneralAsync(CrearBateriaGeneralDto dto);
        public Task<ResultadoOperacionBateria> EditarBateriaGeneralAsync(int grupoId, CrearBateriaGeneralDto dto);
        public Task<ResultadoOperacionBateria> EliminarBateriaGeneralAsync(int grupoId);
        public Task<IEnumerable<int>> GetUsuariosAsignadosAsync(int grupoId);
        public Task<IEnumerable<AsignacionBateriaDto>> GetTodasLasAsignacionesAsync();
    }
}
