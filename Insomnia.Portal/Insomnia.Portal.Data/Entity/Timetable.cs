using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Enums;

namespace Insomnia.Portal.Data.Entity
{
    public class Timetable : Base
    {
        public int LocationId { get; set; }

        public Location Location { get; set; }

        public Day Day { get; set; }
    
        public virtual List<AudienceElement> Audiences { get; set; }
    }
}
