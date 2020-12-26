using System;
using System.Collections.Generic;
using System.Text;

namespace EnvRnk.DataAccess.DbModels
{
    public class RnkUser
    {
        public Guid Id { get; set; }

        public Guid AspUserId { get; set; }
        public AspUser AspUser { get; set; }

        public ICollection<Article> Articles { get; set; }
        public ICollection<UserArticlePoint> ArticlesMarked { get; set; }
    }
}
