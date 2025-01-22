using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface ISendMailService
    {
        void sendEmail(string subject, string body, string to);
    }
}
