using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnvRnk.DataAccess.DbModels
{
    public class Article
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public int Pluses { get; set; }
        public int Minuses { get; set; }
        public string CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        public Guid AuthorId { get; set; }
        public RnkUser Author { get; set; }
        public ICollection<UserArticlePoint> MarkedBy { get; set; }
    }
}
