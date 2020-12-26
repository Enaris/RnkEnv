using EnvRnk.Services.DTOs.RnkUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnvRnk.Services.DTOs.Article
{
    public class ArticleForList
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Pluses { get; set; }
        public int Minuses { get; set; }
        public string CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        public RnkUserForArticleList Author { get; set; }
    }
}
