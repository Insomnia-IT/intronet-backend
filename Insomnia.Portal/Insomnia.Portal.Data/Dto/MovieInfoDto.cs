using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Dto
{
    public record MovieInfo(string Name = null, string Author = null, string Country = null, string Year = null,
        string Duration = null);
}
