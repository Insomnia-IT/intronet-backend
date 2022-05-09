using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class ScheduleReturn : BaseReturn
    {
        public ScheduleReturn() { }

        public ScheduleReturn(IList<TimetableDto> model)
        {
            Model = model;
        }

        public new IList<TimetableDto> Model { get; set; }
    }
}
