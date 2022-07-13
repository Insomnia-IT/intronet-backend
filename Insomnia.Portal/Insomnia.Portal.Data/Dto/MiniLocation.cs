using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Dto
{
    public class MiniLocation
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public List<string> Tags { get; set; }

        public List<string> Directions { get; set; }
    }
}
