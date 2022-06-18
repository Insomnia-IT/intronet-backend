using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Entity;

namespace Insomnia.Portal.General.Expansions
{
    public static class TimetableExpansions
    {
        public static DateTime GetOldCreateTime(this Elementtable element)
        {
            return element.History.IsEmptyOrNull() ? element.CreatedDate : element.ModifiedDate;
        }
    }
}
