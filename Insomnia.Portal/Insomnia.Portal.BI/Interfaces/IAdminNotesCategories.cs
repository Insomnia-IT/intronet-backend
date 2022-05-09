using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.ViewModels.Input;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface IAdminNotesCategories
    {
        Task<NoteCategoryReturn> Add(CreateNoteCategory category);

        Task<NoteCategoryReturn> Edit(EditNoteCategory category);

        Task<NoteCategoryReturn> Delete(int id);
    }
}
