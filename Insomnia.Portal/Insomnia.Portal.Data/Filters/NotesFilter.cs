using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Filters
{
    public class NotesFilter : BaseFilter
    {
        public bool IsSmartFilter { get; set; }

        public int[] CategoriesIds { get; set; }
    }
}