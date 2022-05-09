using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class TimetableReturn : BaseReturn
    {
        public TimetableReturn() { }

        public TimetableReturn(TimetableDto model)
        {
            Model = model;
        }

        public new TimetableDto Model { get; set; }
    }
}
