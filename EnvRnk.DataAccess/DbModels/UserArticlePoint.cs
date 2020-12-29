using System;
using System.Collections.Generic;
using System.Text;

namespace EnvRnk.DataAccess.DbModels
{
    public class UserArticlePoint
    {
        public Guid Id { get; set; }
        public bool Plus { get; set; }

        public Guid UserId { get; set; }
        public RnkUser User { get; set; }
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
