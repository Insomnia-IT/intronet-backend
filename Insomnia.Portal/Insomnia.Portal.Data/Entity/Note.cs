using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class Note : Base2
    {
        public string Header { get; set; }

        public string Title { get; set; }

        public int? CategoryId { get; set; }

        public NoteCategory Category { get; set; }
    }
}
