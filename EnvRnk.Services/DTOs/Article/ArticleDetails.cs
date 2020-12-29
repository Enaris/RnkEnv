using EnvRnk.Services.DTOs.Point;
using EnvRnk.Services.DTOs.RnkUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnvRnk.Services.DTOs.Article
{
    public class ArticleDetails
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Pluses { get; set; }
        public int Minuses { get; set; }
        public string CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public bool UserPlused { get; set; }
        public bool UserMinused { get; set; }

        public Guid AuthorId { get; set; }
        public RnkUserForArticleList Author { get; set; }
        public ICollection<PointForArticleDetails> MarkedBy { get; set; }
    }
}
