using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class DirectionsReturn : BaseReturn
    {
        public DirectionsReturn() { }

        public DirectionsReturn(IList<DirectionDto> model)
        {
            Model = model;
        }

        public new IList<DirectionDto> Model { get; set; }
    }
}

