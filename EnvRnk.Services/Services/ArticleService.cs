using AutoMapper;
using EnvRnk.DataAccess.DbModels;
using EnvRnk.DataAccess.Repositories;
using EnvRnk.Services.DTOs.Article;
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
        private readonly IMapper mapper;

        public ArticleService(IArticleRepository articleRepository,
            IMapper mapper)
        {
            this.articleRepository = articleRepository;
            this.mapper = mapper;
        }

        public async Task<Article> Create(ArticleAddRequest request, Guid rnkUserId, string coverUrl)
        {
            var article = mapper.Map<Article>(request);
            article.AuthorId = rnkUserId;
            article.CoverUrl = coverUrl;

            await articleRepository.CreateAsync(article);
            await articleRepository.SaveChangesAsync();
            return article;
        }

        public async Task<IEnumerable<ArticleForList>> GetForList(ArticleListRequest request)
        {
            var articles = await articleRepository.GetAll(request.Title, request.Email, true).ToListAsync();
            return mapper.Map<IEnumerable<ArticleForList>>(articles);
        }
    }
}
