using EnvRnk.DataAccess.Context;
using EnvRnk.DataAccess.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnvRnk.DataAccess.Repositories
{
    public class RnkUserRepository : BaseRepository<RnkUser>, IRnkUserRepository
    {
        public RnkUserRepository(RnkContext context) : base(context)
        {
        }
    }
}
