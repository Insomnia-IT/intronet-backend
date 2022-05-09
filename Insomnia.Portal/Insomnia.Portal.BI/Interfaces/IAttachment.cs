using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.ViewModels.Input;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface IAttachment
    {
        Task<string> Upload(CreateAttachment attachment);

        Task<AttachmentReturn> Get(Guid guid);

        Task<(MemoryStream, string)> GetFile(Guid guid);
    }
}
