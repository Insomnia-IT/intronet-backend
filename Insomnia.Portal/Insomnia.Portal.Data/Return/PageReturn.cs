using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class PageReturn : BaseReturn
    {
        public PageReturn() { }

        public PageReturn(PageDto model)
        {
            Model = model;
        }

        public new PageDto Model { get; set; }
    }
}
