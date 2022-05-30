using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.BI.Interfaces;
using Insomnia.Portal.Data.Return;

namespace Insomnia.Portal.BI.Services
{
    public class Cartoons : ICartoons
    {
        public Cartoons() { }

        public Task<ReturnCartoonsSchedule> GetSchedule()
        {
            throw new NotImplementedException();
        }
    }
}
