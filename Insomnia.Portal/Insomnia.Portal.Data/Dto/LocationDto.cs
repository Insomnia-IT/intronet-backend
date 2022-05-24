using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Entity;
using Insomnia.Portal.Data.Enums;

namespace Insomnia.Portal.Data.Dto
{
    public class LocationDto : BaseDto
    {
        public string Name { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public string Image { get; set; }

        public TagDto[] Tags { get; set; }

        public string Menu { get; set; }

        public IList<TimetableDto> Timetables { get; set; }
    }
}
