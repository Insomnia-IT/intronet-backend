using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class CartoonsReturn : BaseReturn
    {
        public CartoonsReturn() { }

        public CartoonsReturn(IList<AnimationTimetable> model)
        {
            Model = model;
        }

        public new IList<AnimationTimetable> Model { get; set; }
    }
}

