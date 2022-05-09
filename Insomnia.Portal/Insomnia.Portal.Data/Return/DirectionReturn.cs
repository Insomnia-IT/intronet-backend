using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class DirectionReturn : BaseReturn
    {
        public DirectionReturn() { }

        public DirectionReturn(DirectionDto model)
        {
            Model = model;
        }

        public new DirectionDto Model { get; set; }
    }
}
