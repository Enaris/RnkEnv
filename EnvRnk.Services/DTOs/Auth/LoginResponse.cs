using System;
using System.Collections.Generic;
using System.Text;

namespace EnvRnk.Services.DTOs.Auth
{
    public class LoginResponse
    {
        public string AspUserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
