using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Entity;
using Insomnia.Portal.Data.Enums;
using Insomnia.Portal.Data.Return;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface ITag
    {
        Task<TagReturn> Get(int tag);

        Task<TagsReturn> GetAll();
    }

    public interface IEntityTag
    {
        Tag GetEntity(int tag);

        IList<Tag> GetEntities(int[] tags);

        Tag GetEntityOrCreating(int tag);

        IList<Tag> GetEntitiesOrCreating(int[] tags);
    }
}