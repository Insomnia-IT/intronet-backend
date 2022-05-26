using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Insomnia.Portal.Data.ViewModels.Input
{
    public class CreateNoteCategory
    {
        [Required]
        public string Name { get; set; }

        public string Color { get; set; }
    }
}
