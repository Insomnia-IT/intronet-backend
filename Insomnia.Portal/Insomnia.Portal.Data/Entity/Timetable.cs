using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class Timetable : Base
    {
        public string Name { get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }
    
        public ICollection<Elementtable> Elements { get; set; }
    }
}
