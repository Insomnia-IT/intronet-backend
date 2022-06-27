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
        public int Id { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public new List<EditElementtable> Elements { get; set; }
    }
}