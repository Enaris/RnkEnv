using EnvRnk.DataAccess.DbModels;
using System;
using System.Linq;

namespace EnvRnk.DataAccess.Repositories
{
    public interface IUserArticlePointRepository : IBaseRepository<UserArticlePoint>
    {
        IQueryable<UserArticlePoint> GetAll(bool wAuthors = false,
            bool wArticles = false,
            Guid? articleId = null);
    }
}