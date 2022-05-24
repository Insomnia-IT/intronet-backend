﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.ViewModels.Input
{
    public class EditElementtable : CreateTimetable
    {
        [Required]
        public int Id { get; set; }

        public bool IsCanceled { get; set; }
    }
}