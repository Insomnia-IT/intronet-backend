using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class Attachment : Base2
    {
        public string FileName { get; set; }

        public long Size { get; set; }

        public string TempName { get; set; }

        public Guid Guid { get; set; }
    }
}
