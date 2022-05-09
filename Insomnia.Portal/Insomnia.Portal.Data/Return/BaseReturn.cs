using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;
using Insomnia.Portal.Data.Enums;

namespace Insomnia.Portal.Data.Return
{
    public class BaseReturn
    {
        public BaseReturn() { }

        public bool Success { get; set; }

        public CodeRequest Code { get; set; }

        public string Message { get; set; }

        public object Model { get; set; }
    }
}
