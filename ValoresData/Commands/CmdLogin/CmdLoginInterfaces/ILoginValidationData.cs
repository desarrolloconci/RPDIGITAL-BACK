using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;
using ValorModels.Dtos.LoginDto;
using ValorModels.Models;
using ValorModels.Models.UsersModels;

namespace ValoresData.Commands.CmdLogin.CmdLoginInterfaces
{
    public interface ILoginValidationData
    {
        public Task<IEnumerable<UserResultDto>> GetUsersAsync();
        public Task<ValidateLoginDto> GetLoginValidationData(string userEmail);
        public Task<LoginResponseDto> CreateUserAsync(UserDto userModel);
        public Task<LoginResponseDto> CreateMedicoAsync(RegisterMedicoDto medicoModel);
        public Task<bool> UpdateUserAsync(string userName, UpdateUserDto userModel);

        public Task<UserModel> GetUserAsyncByUsername(string userName);
        public Task<bool> DeleteUserAsync(string userName);
    }
}
