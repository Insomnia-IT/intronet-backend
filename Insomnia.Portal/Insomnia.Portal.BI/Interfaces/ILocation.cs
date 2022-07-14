using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.Filters;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface ILocation
    {
        Task<LocationReturn> Get(int id);

        Task<LocationReturn> GetFull(int id);

        Task<LocationsReturn> GetAll();

        Task<LocationsReturn> GetAllFull();

        Task<LocationsReturn> GetAllWithFilter(LocationsFilter filter);

        Task<LocationsReturn> GetAllFullWithFilter(LocationsFilter filter);

        Data.Entity.Location GetEntity(string name);
    }
}
