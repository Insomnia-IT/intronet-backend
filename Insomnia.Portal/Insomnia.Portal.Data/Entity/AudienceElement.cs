using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class AudienceElement : Base
    {
        public int Number { get; set; }

        public int TimetableId { get; set; }

        public Timetable Timetable { get; set; }

        public virtual List<Elementtable> Elements { get; set; }
    }
}
