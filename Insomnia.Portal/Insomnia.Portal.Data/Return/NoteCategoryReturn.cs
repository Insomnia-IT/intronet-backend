using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class NoteCategoryReturn : BaseReturn
    {
        public NoteCategoryReturn() { }

        public NoteCategoryReturn(NoteCategoryDto model)
        {
            Model = model;
        }

        public new NoteCategoryDto Model { get; set; }
    }
}
