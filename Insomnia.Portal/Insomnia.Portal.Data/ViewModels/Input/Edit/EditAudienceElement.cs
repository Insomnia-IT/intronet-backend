using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Insomnia.Portal.Data.ViewModels.Input
{
    public class EditAudienceElement : CreateAudienceElement
    {
        [Required]
        public int Id { get; set; }

        public new List<EditElementtable> Elementtables { get; set; }
    }
}