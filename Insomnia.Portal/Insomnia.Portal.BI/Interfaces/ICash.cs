using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Entity;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface ICash
    {
        Task<Cash> Get(string name);

        Task<IList<Cash>> GetAll();
    }
}
