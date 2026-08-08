using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos;

namespace ValoresData.Commands
{
   public class UsuariosCmd : IUsuariosCmd
    {
        private readonly DataBaseContext _context;
        private readonly IGeclisaConnectionFactory _geclisaConnectionFactory;

        public UsuariosCmd(DataBaseContext context, IGeclisaConnectionFactory geclisaConnectionFactory)
        {
            _context = context;
            _geclisaConnectionFactory = geclisaConnectionFactory;
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

        // No hay SP de GECLISA para "listar todos los usuarios" (solo BuscarPorID y
        // BuscarPorNombreUsuario, pensados para 1 usuario puntual). Es un catalogo de
        // solo lectura para el picker de admin, asi que se lee directo de la tabla
        // (mismo criterio que ya usa ListadoTurnoCmd para catalogos de otros servidores).
        public async Task<IEnumerable<UsuarioGeclisaListadoDto>> GetUsuariosGeclisaAsync()
        {
            const string sql = @"
                SELECT Usuario_id, Usuario_nom,
                       RTRIM(ISNULL(Usu_Ape,'')) + ' ' + RTRIM(ISNULL(Usu_nom,'')) AS NombreCompleto
                FROM Usuarios
                WHERE ISNULL(Usuario_Inactivo, 0) = 0
                ORDER BY Usu_Ape, Usu_nom";

            using var conn = _geclisaConnectionFactory.CreateConnection();
            return await conn.QueryAsync<UsuarioGeclisaListadoDto>(sql);
        }
    }
}
