using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Services.LoginInterfaces;
using ValorModels.Dtos.LoginDto;


namespace ValoresData.Services.LoginService
{

    public class TokenService : ITokenBuilder
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string BuildToken(LoginTokenDto userData)
        {
            var securtykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securtykey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,userData.Email),
                new Claim(ClaimTypes.Role,userData.Rol),
                new Claim("Rol",userData.Rol),
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
            };
            

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
