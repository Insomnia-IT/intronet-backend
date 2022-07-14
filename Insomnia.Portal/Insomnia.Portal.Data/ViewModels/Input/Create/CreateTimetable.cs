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

    public class CreateTimetableJson
    {
        public string Location { get; set; }

        public List<CreateAudienceElement> Audiences { get; set; } = new List<CreateAudienceElement>();

        public string Day { get; set; }
    }
}
