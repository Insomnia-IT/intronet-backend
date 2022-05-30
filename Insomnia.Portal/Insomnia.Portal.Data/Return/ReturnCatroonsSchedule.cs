using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class ReturnCartoonsSchedule : BaseReturn
    {
        public ReturnCartoonsSchedule() : base()
        { }

        public ReturnCartoonsSchedule(IList<CartoonsScheduleDto> model)
        {
            Model = model;
        }

        public new IList<CartoonsScheduleDto> Model { get; set; }
    }
}
