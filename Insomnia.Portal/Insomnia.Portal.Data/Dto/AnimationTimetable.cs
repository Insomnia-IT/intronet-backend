using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Enums;

namespace Insomnia.Portal.Data.Dto
{
    public class AnimationTimetable
    {
        public Day Day { get; set; }
        public string Screen { get; set; }
        public List<AnimationBlock> Blocks { get; set; }
    }
}
