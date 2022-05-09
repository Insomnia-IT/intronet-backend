using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Insomnia.Portal.Data.ViewModels.Input
{
    public class CreateDirection
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public IFormFile File { get; set; }
    }
}
