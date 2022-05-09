using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class NoteReturn : BaseReturn
    {
        public NoteReturn() { }

        public NoteReturn(NoteDto model)
        {
            Model = model;
        }

        public new NoteDto Model { get; set; }
    }
}
