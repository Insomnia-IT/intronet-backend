using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.ViewModels.Input
{
    public class CreateAudienceElement
    {
        public int Number { get; set; }

        public List<CreateElementtable> Elements { get; set; }
    }
}
