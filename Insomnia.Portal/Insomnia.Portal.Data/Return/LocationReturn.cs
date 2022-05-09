using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;
using Insomnia.Portal.Data.Entity;
using AutoMapper;

namespace Insomnia.Portal.Data.Return
{
    public class LocationReturn : BaseReturn
    {
        public LocationReturn() : base()
        { }

        public LocationReturn(LocationDto model)
        {
            Model = model;
        }
    }
}
