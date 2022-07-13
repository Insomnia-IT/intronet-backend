using Insomnia.Portal.Data.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface ITimetableImport
    {
        Task<ImportReturn> Timetables();
    }
}
