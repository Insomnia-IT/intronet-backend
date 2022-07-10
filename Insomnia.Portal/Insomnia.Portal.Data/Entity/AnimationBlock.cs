using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class AnimationBlock : Base
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string TitleEn { get; set; }
        public string SubTitleEn { get; set; }
        public int MinAge { get; set; }
        public int Part { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        public List<MovieInfo> Movies { get; set; } = new List<MovieInfo>();
    }
}
