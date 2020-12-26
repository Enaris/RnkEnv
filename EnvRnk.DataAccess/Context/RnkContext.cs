using EnvRnk.DataAccess.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnvRnk.DataAccess.Context
{
    public class RnkContext : IdentityDbContext<AspUser>
    {
        public RnkContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<RnkUser> RnkUsers { get; set; }
        public DbSet<UserArticlePoint> Points { get; set; }
    }
}
