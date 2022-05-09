using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface ISchedule
    {
        Task<ScheduleReturn> Get(int locationId);
    }
}
