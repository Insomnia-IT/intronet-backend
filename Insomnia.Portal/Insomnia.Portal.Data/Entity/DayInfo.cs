using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class DayInfo : Base
    {
        public string Date { get; set; }

        public string Screen { get; set; }

        public List<BlockInfo> Blocks { get; set; }
    }
}
