﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface ISender
    {
        Task<T> Get<T>(string url, string token = null);
    }
}
