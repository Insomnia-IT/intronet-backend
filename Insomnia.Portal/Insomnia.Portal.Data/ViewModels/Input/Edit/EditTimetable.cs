using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Enums;

namespace Insomnia.Portal.Data.ViewModels.Input
{
    public class EditTimetable : CreateTimetable
    {
        [Required]
        public int Id { get; set; }

        public new Day? Day { get; set; }

        public new List<EditAudienceElement> Audiences { get; set; }
    }
}
