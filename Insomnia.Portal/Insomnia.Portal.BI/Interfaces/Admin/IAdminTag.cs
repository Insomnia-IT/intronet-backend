using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.ViewModels.Input;
using Insomnia.Portal.Data.Entity;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface IAdminTag
    {
        Task<TagReturn> Add(CreateTag tag);

        Task<int> AddOrGetId(CreateTag tag);

        Task<TagReturn> Edit(EditTag tag);

        Task<TagReturn> Delete(int id);

        Task Clear();
    }
}
