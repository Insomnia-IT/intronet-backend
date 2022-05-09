using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Insomnia.Portal.Data.ViewModels.Input
{
    public class CreateTag
    {
        [Required]
        public string Name { get; set; }
    }
}
