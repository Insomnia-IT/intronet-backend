﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.BI.Options
{
    public class AuthOptions
    {
        public const string Name = "AuthConfig";

        public string AdminToken { get; set; }

        public string PoteryashkiToken { get; set; }
    }
}
