using System;
using System.Collections.Generic;
using System.Text;

namespace EnvRnk.Services.DTOs.Article
{
    public class ArticleListRequest
    {
        public string UserAspId { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
    }
}
