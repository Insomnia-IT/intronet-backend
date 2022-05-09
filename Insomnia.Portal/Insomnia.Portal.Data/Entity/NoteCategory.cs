﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class NoteCategory : Base
    {
        public string Name { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
