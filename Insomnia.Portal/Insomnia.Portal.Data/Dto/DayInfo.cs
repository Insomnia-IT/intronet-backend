using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Dto
{
    public record DayInfo(string Date, string Screen, List<BlockInfo> Blocks);
}
