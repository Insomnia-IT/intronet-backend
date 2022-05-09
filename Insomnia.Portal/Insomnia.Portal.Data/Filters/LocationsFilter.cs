using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Enums;

namespace Insomnia.Portal.Data.Filters
{
    public class LocationsFilter : BaseFilter
    {
        public string Name { get; set; }

        public int[] Tags { get; set; }
    }
}
