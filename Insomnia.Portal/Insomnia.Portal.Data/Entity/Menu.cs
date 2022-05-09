using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class Menu : Base2
    {
        public string Description { get; set; }

        public int Cost { get; set; }

        public virtual ICollection<HistoryElementtable> History { get; set; }

        public bool IsCanceled { get; set; }
    }
}
