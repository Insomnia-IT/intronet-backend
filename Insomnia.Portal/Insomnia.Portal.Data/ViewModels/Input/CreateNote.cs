using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.ViewModels.Input
{
    public class CreateNote
    {
        public string Header { get; set; }

        public string Title { get; set; }

        public int? CategoryId { get; set; }
    }
}
