using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Enums;

namespace Insomnia.Portal.Data.Dto
{
    public class CartoonsScheduleDto
    {
        public int ScreenId { get; set; }

        public Day Day { get; set; }

      //  public IList<CartoonDto> Cartoons { get; set; }
    }
}
