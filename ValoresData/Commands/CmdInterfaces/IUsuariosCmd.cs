using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;

namespace ValoresData.Commands.CmdInterfaces
{
   public interface IUsuariosCmd
    {
        public Task<IEnumerable<UsuarioDto>> GetUsersAsync();

        public Task<IEnumerable<UsuarioGeclisaListadoDto>> GetUsuariosGeclisaAsync();
    }
}
