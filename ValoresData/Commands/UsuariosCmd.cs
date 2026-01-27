using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos;

namespace ValoresData.Commands
{
   public class UsuariosCmd : IUsuariosCmd
    {
        private readonly DataBaseContext _context;
        public UsuariosCmd(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UsuarioDto>> GetUsersAsync()
        {
            var result = await (from v in _context.BH_USERS
                                select new UsuarioDto
                                {
                                    ID = v.ID,
                                    Email = v.Email,
                                    User_name = v.User_name,
                                    Name = v.Name,
                                    Last_name=v.Last_name,
                                    Role= v.Role

                                }).ToListAsync();

            return result;

        }
    }
}

