using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class LocationsReturn : BaseReturn
    {
        public LocationsReturn() : base()
        { }

        public LocationsReturn(IList<LocationDto> model)
        {
            Model = model;
        }

        public new IList<LocationDto> Model { get; set; }
    }
}
