using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.ViewModels.Input
{
    public class CreateTimetable
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int LocationId { get; set; }

        public ICollection<CreateElementtable> Elements { get; set; }
    }
}
