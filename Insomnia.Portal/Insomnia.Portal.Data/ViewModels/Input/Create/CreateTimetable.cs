using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Enums;

namespace Insomnia.Portal.Data.ViewModels.Input
{
    public class CreateTimetable
    {
        public int LocationId { get; set; }

        public List<CreateAudienceElement> Audiences { get; set; }

        public Day Day { get; set; }
    }
}
