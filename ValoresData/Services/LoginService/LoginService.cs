using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdLogin.CmdLoginInterfaces;
using ValoresData.Services.LoginInterfaces;
using ValorModels.Dtos.LoginDto;
using ValorModels.Models.UsersModels;

namespace ValoresData.Services.LoginService
{
    public class LoginService : ILogin
    {
        private readonly ILoginValidationData _validationData;
        private readonly ITokenBuilder _tokenBuilder;
        public LoginService(ILoginValidationData validationData, ITokenBuilder tokenBuilder)
        {
            _tokenBuilder = tokenBuilder;
            _validationData = validationData;
        }

        public async Task<LoginResponseDto> RequestCreateUserAsync(UserDto userModel)
        {
            LoginResponseDto result = await _validationData.CreateUserAsync(userModel);
            return result;
        }

        public async Task<LoginResponseDto> RequestCreateMedicoAsync(RegisterMedicoDto medicoModel)
        {
            LoginResponseDto result = await _validationData.CreateMedicoAsync(medicoModel);
            return result;
        }

        public async Task<bool> RequestUpdateUserAsync(string userName, UpdateUserDto userModel)
        {
            return await _validationData.UpdateUserAsync(userName, userModel);
        }

        public async Task<LoginResponseDto> RequestLoginAsync(LoginDto userCredentials)
        {
            ValidateLoginDto userData = await _validationData.GetLoginValidationData(userCredentials.Username);

            if (userData != null)
            {
                if (!IsPasswordValid(userData, userCredentials)) return null;

                var loginTokenDto = new LoginTokenDto
                {
                    Email = userCredentials.Username,
                    Password = userCredentials.Password,
                    Rol = userData.Role

                };
                string token = _tokenBuilder.BuildToken(loginTokenDto);
                return new LoginResponseDto()
                {
                    ID=userData.ID,
                    UserName = userData.UserName,
                    Token = token,
                    FIRMA=userData.FIRMA,
                };
            }
            else
            {
                return null;
            }
        }
        private static bool IsPasswordValid(ValidateLoginDto userData, LoginDto userCredentials)
        {
            string hashedPwd = BCrypt.Net.BCrypt.HashPassword(userCredentials.Password, userData.Salt);
            return userData.Password == hashedPwd;
        }

        public async Task<IEnumerable<UserResultDto>> GetUsersAsync()
        {
            return await _validationData.GetUsersAsync();
        }
        public async Task<UserModel> GetUserAsyncByUsername(string userName)
        {
            return await _validationData.GetUserAsyncByUsername(userName);
        }
        public Task<bool> DeleteUserAsync(string userName)
        {
            return _validationData.DeleteUserAsync(userName);
        }
    }
}
