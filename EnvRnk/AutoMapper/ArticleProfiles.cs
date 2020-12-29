using AutoMapper;
using EnvRnk.DataAccess.DbModels;
using EnvRnk.Services.DTOs.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvRnk.AutoMapper
{
    public class ArticleProfiles : Profile
    {
        public ArticleProfiles()
        {
            CreateMap<ArticleAddRequest, Article>();
            CreateMap<Article, ArticleForList>();
            CreateMap<Article, ArticleDetails>();
        }
    }
}
