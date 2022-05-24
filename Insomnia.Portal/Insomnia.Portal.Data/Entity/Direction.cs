using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class Direction : BaseCashing
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}
