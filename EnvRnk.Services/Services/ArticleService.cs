using AutoMapper;
using EnvRnk.DataAccess.DbModels;
using EnvRnk.DataAccess.Repositories;
using EnvRnk.Services.DTOs.Article;
using EnvRnk.Services.DTOs.Point;
using EnvRnk.Services.DTOs.RnkUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnvRnk.Services.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository articleRepository;
        private readonly IUserArticlePointRepository userArticlePointRepo;
        private readonly IMapper mapper;

        public ArticleService(IArticleRepository articleRepository,
            IUserArticlePointRepository userArticlePointRepo,
            IMapper mapper)
        {
            this.articleRepository = articleRepository;
            this.userArticlePointRepo = userArticlePointRepo;
            this.mapper = mapper;
        }

        public async Task<Article> Create(ArticleAddRequest request, Guid rnkUserId, string coverUrl)
        {
            var article = mapper.Map<Article>(request);
            article.AuthorId = rnkUserId;
            article.CoverUrl = coverUrl;
            article.DateAdded = DateTime.UtcNow;

            await articleRepository.CreateAsync(article);
            await articleRepository.SaveChangesAsync();
            return article;
        }

        public async Task<IEnumerable<ArticleForList>> GetForList(string title, 
            string email, 
            Guid? rnkUserId = null, 
            bool newArticles = true, 
            bool ranking = false, 
            bool trending = false)
        {
            var articles = await articleRepository
                .GetAll(title, email, true, newArticles, ranking, trending)
                .ToListAsync();
            var result = mapper.Map<IEnumerable<ArticleForList>>(articles);
            if (rnkUserId == null)
                return result;

            foreach (var a in result)
            {
                var point = await userArticlePointRepo
                    .GetAll()
                    .FirstOrDefaultAsync(p => p.UserId == rnkUserId.Value && p.ArticleId == a.Id);
                if (point == null)
                    continue;
                a.UserPlused = point.Plus;
                a.UserMinused = !point.Plus;
            }

            return result;
        }
    
        public async Task<ArticleForList> PointArticle(Guid articleId, Guid rnkUserId, bool plus)
        {
            var article = await articleRepository
                .GetAll(null, null, true)
                .FirstOrDefaultAsync(a => a.Id == articleId);

            if (article == null)
                return null;

            var point = await userArticlePointRepo
                .GetAll()
                .FirstOrDefaultAsync(uap => uap.ArticleId == articleId && uap.UserId == rnkUserId);
          
            if (point != null)
            {
                if (point.Plus == plus)
                    return null;

                article.Pluses += point.Plus ? -1 : 0;
                article.Minuses += !point.Plus ? -1 : 0;
                userArticlePointRepo.Delete(point);
            }

            var newPoint = new UserArticlePoint
            {
                ArticleId = articleId,
                UserId = rnkUserId,
                Plus = plus,
            };

            article.Pluses += plus ? 1 : 0;
            article.Minuses += !plus ? 1 : 0;
            articleRepository.Update(article);
            await articleRepository.SaveChangesAsync();            

            await userArticlePointRepo.CreateAsync(newPoint);
            await userArticlePointRepo.SaveChangesAsync();
            var result = mapper.Map<ArticleForList>(article);
            result.UserMinused = !plus;
            result.UserPlused = plus;

            return result;
        }
    
        public async Task<ArticleForList> RemoveScore(Guid articleId, Guid rnkUserId)
        {
            var article = await articleRepository
                .GetAll(null, null, true)
                .FirstOrDefaultAsync(a => a.Id == articleId);

            if (article == null)
                return null;

            var point = await userArticlePointRepo
                .GetAll()
                .FirstOrDefaultAsync(uap => uap.ArticleId == articleId && uap.UserId == rnkUserId);

            if (point == null)
                return mapper.Map<ArticleForList>(article);

            userArticlePointRepo.Delete(point);
            await userArticlePointRepo.SaveChangesAsync();

            if (point.Plus)
                --article.Pluses;
            else
                --article.Minuses;

            articleRepository.Update(article);
            await articleRepository.SaveChangesAsync();

            var result = mapper.Map<ArticleForList>(article);
            result.UserMinused = result.UserPlused = false;
            return result;
        }

        public async Task<ArticleDetails> GetDetails(Guid articleId, Guid? rnkUserId)
        {
            var article = await articleRepository
                .GetAll(null, null, true)
                .FirstOrDefaultAsync(a => a.Id == articleId);

            if (article == null)
                return null;

            var result = mapper.Map<ArticleDetails>(article);
            var articlePoints = await userArticlePointRepo
                .GetAll(true, false, articleId)
                .ToListAsync();
            var markedBy = new List<PointForArticleDetails>(articlePoints.Count);

            foreach (var point in articlePoints)
            {
                if (rnkUserId != null && point.UserId == rnkUserId)
                {
                    result.UserPlused = point.Plus;
                    result.UserMinused = !point.Plus;
                }
                if (point.UserId != rnkUserId)
                    markedBy.Add(mapper.Map<PointForArticleDetails>(point));
            }

            result.MarkedBy = markedBy;
            return result;
        }
    
        public async Task<Article> GetArticleDb(Guid articleId, Guid rnkUserId)
        {
            return await articleRepository
                .GetAll()
                .FirstOrDefaultAsync(a => a.AuthorId == rnkUserId && a.Id == articleId);
        }
        public async Task Delete(Article article)
        {
            if (article == null)
                return;

            var articlePoints = await userArticlePointRepo
                .GetAll(ap => ap.ArticleId == article.Id)
                .ToListAsync();
            foreach (var ap in articlePoints)
                userArticlePointRepo.Delete(ap);
            await userArticlePointRepo.SaveChangesAsync();

            articleRepository.Delete(article);
            await articleRepository.SaveChangesAsync();
        }
    }
}
