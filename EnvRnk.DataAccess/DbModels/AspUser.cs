﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnvRnk.DataAccess.DbModels
{
    public class AspUser : IdentityUser
    {
        [Required]
        public override string Email { get; set; }
    }
}
