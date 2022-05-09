using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.BI.Options
{
    public class AttachmentOptions
    {
        public const string Name = "AttachmentsConfig";

        public string Path { get; set; }

        public string UrlAttachmentService { get; set; }
    }
}
