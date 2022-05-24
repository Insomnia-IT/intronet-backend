using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Dto
{
    public class ElementtableDto
    {
        public string Description { get; set; }

        public DateTime Time { get; set; }

        public string Speaker { get; set; }

        public bool IsCanceled { get; set; }

        public string Changes { get; set; }
    }
}
