using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;

namespace ValoresData.Services
{
    public class UsuariosService : IUsusarioService
    {
        private readonly IUsuariosCmd _cmd;
        public UsuariosService(IUsuariosCmd cmd)
        {
            _cmd = cmd;
        }
        public async Task<IEnumerable<UsuarioDto>> GetUsersAsync()
        {
            return await _cmd.GetUsersAsync();
        }

        public async Task<IEnumerable<UsuarioGeclisaListadoDto>> GetUsuariosGeclisaAsync()
        {
            return await _cmd.GetUsuariosGeclisaAsync();
        }
    }
}
