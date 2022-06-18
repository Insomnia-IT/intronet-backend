using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.ViewModels.Input;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface IAdminTag
    {
        Task<TagReturn> Add(CreateTag tag);

        Task<TagReturn> Edit(EditTag tag);

        Task<TagReturn> Delete(int id);
    }
}
