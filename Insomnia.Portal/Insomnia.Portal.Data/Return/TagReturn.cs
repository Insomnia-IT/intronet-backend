using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class TagReturn : BaseReturn
    {
        public TagReturn() { }

        public TagReturn(TagDto model)
        {
            Model = model;
        }

        public new TagDto Model { get; set; }
    }
}
