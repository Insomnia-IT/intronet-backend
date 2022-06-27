using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Dto
{
    public class AudienceElementDto
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public IList<ElementtableDto> Elements { get; set; }
    }
}
