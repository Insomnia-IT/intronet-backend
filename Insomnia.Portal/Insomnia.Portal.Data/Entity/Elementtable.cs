using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class Elementtable : Base2
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Time { get; set; }

        public string Speaker { get; set; }

        public int AudienceId { get; set; }

        public AudienceElement Audience { get; set; }

        public virtual List<HistoryElementtable> History { get; set; }

        public bool IsCanceled { get; set; } = false;
    }
}
