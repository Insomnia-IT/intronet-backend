using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Entity;
using Insomnia.Portal.Data.Enums;
using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace Insomnia.Portal.Data.Dto
{
    [AutoMap(typeof(Location))]
    public class LocationDto : BaseDto
    {
        public string Name { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public string Image { get; set; }

        public TagDto[] Tags { get; set; }
    }
}
