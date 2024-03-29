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

        public string Title { get; set; }

        public string Text { get; set; }

        public int CategoryId { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
