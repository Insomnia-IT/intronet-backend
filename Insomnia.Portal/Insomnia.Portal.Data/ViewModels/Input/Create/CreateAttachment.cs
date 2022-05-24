using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Insomnia.Portal.Data.ViewModels.Input
{
    public class CreateAttachment
    {
        public IFormFile File { get; set; }
    }
}
