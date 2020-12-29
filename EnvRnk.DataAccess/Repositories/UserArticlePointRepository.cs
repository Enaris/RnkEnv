using EnvRnk.DataAccess.Context;
using EnvRnk.DataAccess.DbModels;
using EnvRnk.DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvRnk.DataAccess.Repositories
{
    public class UserArticlePointRepository : BaseRepository<UserArticlePoint>, IUserArticlePointRepository
    {
        public UserArticlePointRepository(RnkContext context) : base(context)
        {
        }

        public IQueryable<UserArticlePoint> GetAll(bool wAuthors = false, 
            bool wArticles = false, 
            Guid? articleId = null)
        {
            return _context.Set<UserArticlePoint>()
                .IfAction(wAuthors, q => q.Include(p => p.User).ThenInclude(a => a.AspUser))
                .IfAction(wArticles, q => q.Include(p => p.Article))
                .IfAction(articleId != null, q => q.Where(uap => uap.ArticleId == articleId));
        }
    }
}
