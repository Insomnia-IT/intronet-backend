using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.Filters;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface INotesboard
    {
        Task<NotesboardReturn> GetAll();

        Task<NotesboardReturn> GetAllWithFilter(NotesFilter filter);

        Task<NoteReturn> Get(int id);
    }
}
