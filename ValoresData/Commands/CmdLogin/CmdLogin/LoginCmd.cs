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
using ValorModels.Models.RpModels;
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

            var newUser = new UserModel
            {
                Name = userModel.Name,
                Last_name = userModel.Last_name,
                Email=userModel.Email,
                User_name = userModel.User_name,
                Password = userModel.Password,
                Role = userModel.Role,
                Salt =salt
            };
            // Agrega el usuario al contexto
            _context.BH_USERS.Add(newUser);

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            return new LoginResponseDto
            {
                ID = newUser.ID,
                UserName = userModel.Name,
                Token = _tokenBuilder.BuildToken(new LoginTokenDto { Email = userModel.Email, Password = hashedPassword, Rol=userModel.Role }),
                FIRMA = newUser.FIRMA
            };
        }

        public async Task<LoginResponseDto> CreateMedicoAsync(RegisterMedicoDto medicoModel)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(medicoModel.Password, salt);

                var newUser = new UserModel
                {
                    Name = medicoModel.Name,
                    Last_name = medicoModel.Last_name,
                    Email = medicoModel.Email,
                    User_name = medicoModel.User_name,
                    Password = hashedPassword,
                    Role = medicoModel.Role,
                    Salt = salt,
                    FIRMA = medicoModel.Firma,
                    Matricula = medicoModel.Matricula
                };
                _context.BH_USERS.Add(newUser);
                await _context.SaveChangesAsync();

                foreach (var especialidadId in medicoModel.EspecialidadesIds)
                {
                    _context.REL_ESP_SERVICIOS.Add(new RelEspServicioModel
                    {
                        usuario_id = newUser.ID,
                        servicio_id = especialidadId
                    });
                }

                foreach (var matricula in medicoModel.Matriculas)
                {
                    _context.Rel_esp_matriculas.Add(new RelEspMatriculasModel
                    {
                        usuario_id = newUser.ID,
                        matricula = matricula
                    });
                }

                foreach (var bateriaId in medicoModel.BateriasIds)
                {
                    _context.Rel_esp_baterias.Add(new RelBateriasEspModel
                    {
                        usuario_id = newUser.ID,
                        bateria_id = bateriaId
                    });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new LoginResponseDto
                {
                    ID = newUser.ID,
                    UserName = newUser.Name,
                    Token = _tokenBuilder.BuildToken(new LoginTokenDto { Email = newUser.Email, Password = hashedPassword, Rol = newUser.Role }),
                    FIRMA = newUser.FIRMA
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateUserAsync(string userName, UpdateUserDto userModel)
        {
            var user = await _context.BH_USERS.Where(e => e.User_name == userName).FirstOrDefaultAsync();
            if (user is null)
            {
                return false;
            }

            user.Name = userModel.Name;
            user.Last_name = userModel.Last_name;
            user.Email = userModel.Email;
            user.Role = userModel.Role;
            user.Matricula = userModel.Matricula;

            if (!string.IsNullOrEmpty(userModel.Password))
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                user.Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password, salt);
                user.Salt = salt;
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<ValidateLoginDto> GetLoginValidationData(string username)
        {
            var result = await _context.BH_USERS.Where(e => e.User_name == username)
                         .Select(e => new ValidateLoginDto
                         {   ID=e.ID,
                             Password = e.Password,
                             Salt = e.Salt,
                             UserName = e.Last_name +" "+ e.Name,
                             //UserName=e.User_name,
                             Role=e.Role,
                             FIRMA=e.FIRMA
                         }).FirstOrDefaultAsync();


            return result;
        }

        public async Task<IEnumerable<UserResultDto>> GetUsersAsync()
        {
            var result = await (from v in _context.BH_USERS
                                select new UserResultDto {

                Email = v.Email,
                User_name =v.User_name,
                Name=v.Name,
                Last_name=v.Last_name,
                Role=v.Role,
                Matricula=v.Matricula
                }).ToListAsync();

            return result;

        }

        public async Task<UserModel> GetUserAsyncByUsername(string userName)
        {
            var result = await _context.BH_USERS.Where(e => e.User_name.Equals(userName)).FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteUserAsync(string userName)

        {
            var dbExcepcion = await GetUserAsyncByUsername(userName);
            if (dbExcepcion is null)
            {
                return false;
            }
            _context.BH_USERS.Remove(dbExcepcion);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
