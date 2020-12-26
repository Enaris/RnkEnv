using EnvRnk.DataAccess.DbModels;
using EnvRnk.Services.DTOs.Article;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnvRnk.Services.Services
{
    public interface IArticleService
    {
        Task<Article> Create(ArticleAddRequest request, Guid rnkUserId, string coverUrl);
        Task<IEnumerable<ArticleForList>> GetForList(ArticleListRequest request);
    }
}