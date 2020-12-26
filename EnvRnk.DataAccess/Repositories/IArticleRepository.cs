using EnvRnk.DataAccess.DbModels;
using System.Linq;

namespace EnvRnk.DataAccess.Repositories
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        IQueryable<Article> GetAll(string title = null, string email = null, bool withAuthors = false);
    }
}