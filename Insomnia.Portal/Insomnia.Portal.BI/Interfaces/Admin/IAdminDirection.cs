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
        Task<int> AddOrGetId(CreateDirection direction);

        Task<DirectionReturn> Add(CreateDirection direction);

        Task<DirectionReturn> Edit(EditDirection direction);

        Task<DirectionReturn> Delete(int id);

        Task Clear();
    }
}
