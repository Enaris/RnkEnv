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
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(RnkContext context) : base(context)
        {
        }

        public IQueryable<Article> GetAll(string title = null, string email = null, bool withAuthors = false)
        {
            return _context.Set<Article>()
                .IfAction(withAuthors, q => q.Include(a => a.Author).ThenInclude(author => author.AspUser))
                .IfAction(!string.IsNullOrWhiteSpace(title), q => q.Where(a => a.Title.Contains(title)))
                .IfAction(!string.IsNullOrWhiteSpace(email), q => q.Where(a => a.Author.AspUser.Email.Contains(email)));
        }

    }
}
