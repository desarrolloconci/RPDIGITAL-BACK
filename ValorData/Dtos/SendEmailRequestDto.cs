using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Dtos
{
    public record SendEmailRequestDto
    (
        string Subject,
        string Body,
        string To
    );
}
