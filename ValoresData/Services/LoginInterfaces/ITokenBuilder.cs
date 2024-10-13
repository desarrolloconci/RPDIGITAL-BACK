using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos.LoginDto;


namespace ValoresData.Services.LoginInterfaces
{
    public interface ITokenBuilder
    {
        string BuildToken(LoginTokenDto userData);
    }
}
