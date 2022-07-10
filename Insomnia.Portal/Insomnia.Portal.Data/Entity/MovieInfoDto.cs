using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Entity
{
    public class MovieInfo : Base
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public string Country { get; set; }

        public string Year { get; set; }

        public string Duration { get; set; }
    }
}