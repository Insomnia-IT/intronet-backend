using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class PagesReturn : BaseReturn
    {
        public PagesReturn() { }

        public PagesReturn(IList<PageDto> model)
        {
            Model = model;
        }

        public new IList<PageDto> Model { get; set; }
    }
}

