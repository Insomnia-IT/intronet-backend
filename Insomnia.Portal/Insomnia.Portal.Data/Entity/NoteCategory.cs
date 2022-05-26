using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class NoteCategory : BaseCashing
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
