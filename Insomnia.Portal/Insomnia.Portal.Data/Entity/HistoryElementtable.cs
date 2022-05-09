using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class HistoryElementtable : Base
    {
        public int TimetableId { get; set; }

        public string OldValue { get; set; }

        public DateTime CreatedDateTimeOldValue { get; set; }
    }
}
