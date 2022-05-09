using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Enums;

namespace Insomnia.Portal.Data.ViewModels.Input
{
    public class CreateLocation
    {
        public string Name { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public int MainTagId { get; set; }

        public int[] Tags { get; set; }
    }
}
