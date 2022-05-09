using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class Location : Base
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public int MainTagId { get; set; }

        public Direction Direction { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Timetable> Timetables { get; set; }
    }
}
