using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Enums;

namespace Insomnia.Portal.Data.Entity
{
    public class HistoryElementtable : Base2
    {
        public int ElementtableId { get; set; }

        public virtual Elementtable Elementtable { get; set; }

        public PropertyElementHistory Type { get; set; }

        public string OldValue { get; set; }
    }
}
