using EnvRnk.DataAccess.Context;
using EnvRnk.DataAccess.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnvRnk.DataAccess.Repositories
{
    public class UserArticlePointRepository : BaseRepository<UserArticlePoint>, IUserArticlePointRepository
    {
        public UserArticlePointRepository(RnkContext context) : base(context)
        {
        }
    }
}
