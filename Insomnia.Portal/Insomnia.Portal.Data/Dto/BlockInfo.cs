using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Dto
{
    public record BlockInfo
    {
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        public List<MovieInfo> Movies = new List<MovieInfo>();
    }
}
