﻿using Insomnia.Portal.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Return
{
    public class CartoonReturn : BaseReturn
    {
        public CartoonReturn() { }

        public CartoonReturn(List<AnimationTimetable> model)
        {
            Model = model;
        }

        public new List<AnimationTimetable> Model { get; set; }
    }
}
