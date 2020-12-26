using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnvRnk.Services.DTOs.Article
{
    public class ArticleAddRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Cover { get; set; }

        public Guid AspAuthorId { get; set; }
    }
}
