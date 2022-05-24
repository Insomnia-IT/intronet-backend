using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class Note : BaseCashing2
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public int CategoryId { get; set; }

        public NoteCategory Category { get; set; }
    }
}
