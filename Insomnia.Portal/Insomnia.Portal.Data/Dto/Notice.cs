﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Dto
{
    public class NoteDto
    {
        public int Id { get; set; }

        public string Header { get; set; }

        public string Title { get; set; }

        public int? CategoryId { get; set; }
    }
}
