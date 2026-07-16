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

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                user.Name = userModel.Name;
                user.Last_name = userModel.Last_name;
                user.Email = userModel.Email;
                user.Role = userModel.Role;
                user.Matricula = userModel.Matricula;
                user.FIRMA = userModel.Firma;

                if (!string.IsNullOrEmpty(userModel.Password))
                {
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    user.Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password, salt);
                    user.Salt = salt;
                }

                // Especialidades, matriculas asociadas y baterias se reemplazan por completo con lo
                // que llega en el DTO, igual que en el alta (CreateMedicoAsync).
                var especialidadesActuales = await _context.REL_ESP_SERVICIOS.Where(e => e.usuario_id == user.ID).ToListAsync();
                _context.REL_ESP_SERVICIOS.RemoveRange(especialidadesActuales);
                foreach (var especialidadId in userModel.EspecialidadesIds)
                {
                    _context.REL_ESP_SERVICIOS.Add(new RelEspServicioModel { usuario_id = user.ID, servicio_id = especialidadId });
                }

                var matriculasActuales = await _context.Rel_esp_matriculas.Where(m => m.usuario_id == user.ID).ToListAsync();
                _context.Rel_esp_matriculas.RemoveRange(matriculasActuales);
                foreach (var matricula in userModel.Matriculas)
                {
                    _context.Rel_esp_matriculas.Add(new RelEspMatriculasModel { usuario_id = user.ID, matricula = matricula });
                }

                var bateriasActuales = await _context.Rel_esp_baterias.Where(b => b.usuario_id == user.ID).ToListAsync();
                _context.Rel_esp_baterias.RemoveRange(bateriasActuales);
                foreach (var bateriaId in userModel.BateriasIds)
                {
                    _context.Rel_esp_baterias.Add(new RelBateriasEspModel { usuario_id = user.ID, bateria_id = bateriaId });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
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
            var usuarios = await _context.BH_USERS.ToListAsync();

            // Se traen las 3 relaciones completas y se agrupan en memoria (en vez de una consulta
            // por usuario) para no hacer N+1 llamadas a la base.
            var especialidadesPorUsuario = (await _context.REL_ESP_SERVICIOS.ToListAsync()).ToLookup(e => e.usuario_id);
            var matriculasPorUsuario = (await _context.Rel_esp_matriculas.ToListAsync()).ToLookup(m => m.usuario_id);
            var bateriasPorUsuario = (await _context.Rel_esp_baterias.ToListAsync()).ToLookup(b => b.usuario_id);

            return usuarios.Select(u => new UserResultDto
            {
                ID = u.ID,
                Email = u.Email,
                User_name = u.User_name,
                Name = u.Name,
                Last_name = u.Last_name,
                Role = u.Role,
                Matricula = u.Matricula,
                Firma = u.FIRMA,
                EspecialidadesIds = especialidadesPorUsuario[u.ID].Select(e => e.servicio_id).ToList(),
                Matriculas = matriculasPorUsuario[u.ID].Select(m => m.matricula).ToList(),
                BateriasIds = bateriasPorUsuario[u.ID].Select(b => b.bateria_id).ToList()
            }).ToList();
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
