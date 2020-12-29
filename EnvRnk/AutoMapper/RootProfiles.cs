using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvRnk.AutoMapper
{
    public class RootProfiles
    {
        public static Type[] Maps => new[]
        {
            typeof(ArticleProfiles), 
            typeof(RnkUserProfiles), 
            typeof(UserArticlePointProfiles)
        };
    }
}
