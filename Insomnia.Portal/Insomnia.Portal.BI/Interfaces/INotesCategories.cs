using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.Entity;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface INotesCategories
    {
        Task<NoteCategoryReturn> Get(int categoryId);

        Task<NoteCategoriesReturn> GetAll();
    }

    public interface IEntityNotesCategories
    {
        NoteCategory GetEntity(int tag);

        IList<NoteCategory> GetEntities(int[] tags);

        NoteCategory GetEntityOrCreating(int tag);

        IList<NoteCategory> GetEntitiesOrCreating(int[] tags);
    }
}
