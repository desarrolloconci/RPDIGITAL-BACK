using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos.LoginDto;
using ValorModels.Models.UsersModels;

namespace ValoresData.Services.LoginInterfaces
{
    public interface ILogin
    {
        public Task<IEnumerable<UserResultDto>> GetUsersAsync();
        Task<LoginResponseDto> RequestLoginAsync(LoginDto userCredentials);
        Task<LoginResponseDto> RequestCreateUserAsync(UserDto userModel);
        public Task<UserModel> GetUserAsyncByUsername(string userName);
        public Task<bool> DeleteUserAsync(string userName);
    }
}
