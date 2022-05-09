using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Enums;

namespace Insomnia.Portal.Data.Entity
{
    public class Tag : Base
    {
        public string Name { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}
