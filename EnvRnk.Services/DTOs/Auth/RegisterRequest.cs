﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EnvRnk.Services.DTOs.Auth
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
