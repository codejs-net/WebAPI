﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Auth
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
    }
}
