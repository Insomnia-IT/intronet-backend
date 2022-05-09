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
    public interface IDirection
    {
        Task<DirectionReturn> Get(int directionId);

        Task<DirectionsReturn> GetAll();
    }

    public interface IEntityDirection
    {
        Direction GetEntity(int directionId);

        IList<Direction> GetEntities(int[] directionsIds);

        Direction GetEntityOrCreating(int directionId);

        IList<Direction> GetEntitiesOrCreating(int[] directionsIds);
    }
}