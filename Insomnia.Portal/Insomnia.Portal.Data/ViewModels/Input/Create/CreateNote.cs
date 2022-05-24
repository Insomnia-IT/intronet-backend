using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Generic;

namespace Insomnia.Portal.Data.ViewModels.Input
{
    public class CreateNote
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public int CategoryId { get; set; } = StaticValues.DefaultIdNoteCategories;
    }
}
