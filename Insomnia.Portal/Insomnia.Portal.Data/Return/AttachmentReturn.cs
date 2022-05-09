using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.Data.Return
{
    public class AttachmentReturn : BaseReturn
    {
        public AttachmentReturn() { }

        public AttachmentReturn(AttachmentDto model)
        {
            Model = model;
        }

        public new AttachmentDto Model { get; set; }
    }
}
