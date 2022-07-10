using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.Entity;
using System.IO;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface IAdminCartoons
    {
        Task<BaseReturn> Add(Stream stream);
    }
}
