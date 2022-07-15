using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface ICartoons
    {
        Task<CartoonsReturn> GetAll();

        Task<CartoonReturn> Get(int id);
    }
}
