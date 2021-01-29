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
        Task<IEnumerable<ArticleForList>> GetForList(string title,
            string email,
            Guid? rnkUserId = null,
            bool newArticles = true,
            bool ranking = false,
            bool trending = false);
        Task<ArticleForList> PointArticle(Guid articleId, Guid rnkUserId, bool plus);
        Task<ArticleForList> RemoveScore(Guid articleId, Guid rnkUserId);
        Task<ArticleDetails> GetDetails(Guid articleId, Guid? rnkUserId);
        Task Delete(Article article);
        Task<Article> GetArticleDb(Guid articleId, Guid rnkUserId);
    }
}