using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.General.Expansions
{
    public static class ReturnModelExpanstions
    {
        public static T ToReturn<T>(this object entity) where T : BaseReturn, new()
        {
            return new T() { Model = entity };
        }
    }
}
