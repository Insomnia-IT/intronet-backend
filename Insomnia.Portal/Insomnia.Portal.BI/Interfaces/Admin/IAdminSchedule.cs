using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.Dto;
using Insomnia.Portal.Data.ViewModels.Input;


namespace Insomnia.Portal.BI.Interfaces
{
    public interface IAdminSchedule
    {
        Task<TimetableReturn> AddOrEdit(EditTimetable timetable);

        Task<TimetableReturn> Delete(int id);
    }
}
