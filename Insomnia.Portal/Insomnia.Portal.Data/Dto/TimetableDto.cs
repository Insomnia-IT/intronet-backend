using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Enums;

namespace Insomnia.Portal.Data.Dto
{
    public class TimetableDto
    {
        public int Id { get; set; }

        public List<AudienceElementDto> Audiences { get; set; }

        public Day Day { get; set; }
    }
}
