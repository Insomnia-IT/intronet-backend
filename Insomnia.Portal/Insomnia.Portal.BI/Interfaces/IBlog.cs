using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface IBlog
    {
        Task<PageReturn> Get(int id);

        Task<PagesReturn> GetAll();
    }
}
