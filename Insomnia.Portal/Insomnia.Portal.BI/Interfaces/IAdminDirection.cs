using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.ViewModels.Input;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface IAdminDirection
    {
        Task<DirectionReturn> Add(CreateDirection directionId);

        Task<DirectionReturn> Edit(EditDirection directionId);

        Task<DirectionReturn> Delete(int id);
    }
}
