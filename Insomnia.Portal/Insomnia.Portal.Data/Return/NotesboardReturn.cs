using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class NotesboardReturn : BaseReturn
    {
        public NotesboardReturn() { }

        public NotesboardReturn(IList<NoteDto> model)
        {
            Model = model;
        }

        public new IList<NoteDto> Model { get; set; }
    }
}
