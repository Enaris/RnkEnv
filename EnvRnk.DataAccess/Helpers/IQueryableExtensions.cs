using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvRnk.DataAccess.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> IfAction<T>(
            this IQueryable<T> source,
            bool condition,
            Func<IQueryable<T>, IQueryable<T>> transform
        )
        {
            return condition ? transform(source) : source;
        }
    }
}
