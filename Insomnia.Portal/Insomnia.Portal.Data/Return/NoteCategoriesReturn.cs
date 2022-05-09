using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class NoteCategoriesReturn : BaseReturn
    {
        public NoteCategoriesReturn() { }

        public NoteCategoriesReturn(IList<NoteCategoryDto> model)
        {
            Model = model;
        }

        public new IList<NoteCategoryDto> Model { get; set; }
    }
}

