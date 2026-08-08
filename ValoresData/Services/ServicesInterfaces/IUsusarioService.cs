using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IUsusarioService
    {
        public Task<IEnumerable<UsuarioDto>> GetUsersAsync();

        public Task<IEnumerable<UsuarioGeclisaListadoDto>> GetUsuariosGeclisaAsync();
    }
}
