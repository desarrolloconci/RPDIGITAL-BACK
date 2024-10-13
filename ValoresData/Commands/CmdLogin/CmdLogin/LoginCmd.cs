using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdLogin.CmdLoginInterfaces;
using ValoresData.Context;
using ValoresData.Services.LoginInterfaces;
using ValorModels.Dtos.LoginDto;
using ValorModels.Models.UsersModels;

namespace ValoresData.Commands.CmdLogin.CmdLogin
{
    public class LoginCmd : ILoginValidationData
    {
        private readonly DataBaseContext _context;
        private readonly ITokenBuilder _tokenBuilder;
        public LoginCmd(DataBaseContext context, ITokenBuilder tokenBuilder)
        {
            _context = context;
            _tokenBuilder= tokenBuilder;
        }
        public async Task<LoginResponseDto> CreateUserAsync(UserDto userModel)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userModel.Password, salt);
            userModel.Password = hashedPassword;
           
            // Agrega el usuario al contexto
            _context.Users.Add(new UserModel
            {
                Name = userModel.Name,
                Last_name = userModel.Last_name,
                Email=userModel.Email,
                User_name = userModel.User_name,
                Password = userModel.Password,
                Role = userModel.Role,
                Salt =salt
            });

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            return new LoginResponseDto
            {
                UserName = userModel.Name,
                Token = _tokenBuilder.BuildToken(new LoginTokenDto { Email = userModel.Email, Password = hashedPassword, Rol=userModel.Role })
            };
        }


        public async Task<ValidateLoginDto> GetLoginValidationData(string username)
        {
            var result = await _context.Users.Where(e => e.User_name == username)
                         .Select(e => new ValidateLoginDto
                         {
                             Password = e.Password,
                             Salt = e.Salt,
                             UserName = e.User_name,
                             Role=e.Role
                         }).FirstOrDefaultAsync();


            return result;
        }

        public async Task<IEnumerable<UserResultDto>> GetUsersAsync()
        {
            var result = await (from v in _context.Users select new UserResultDto {

                Email = v.Email,
                User_name =v.User_name,
                Name=v.Name,
                Last_name=v.Last_name,
                Role=v.Role
                }).ToListAsync();

            return result;

        }

        public async Task<UserModel> GetUserAsyncByUsername(string userName)
        {
            var result = await _context.Users.Where(e => e.User_name.Equals(userName)).FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteUserAsync(string userName)

        {
            var dbExcepcion = await GetUserAsyncByUsername(userName);
            if (dbExcepcion is null)
            {
                return false;
            }
            _context.Users.Remove(dbExcepcion);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
