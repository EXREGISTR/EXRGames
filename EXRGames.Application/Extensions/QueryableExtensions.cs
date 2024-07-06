using System.Linq.Expressions;

namespace EXRGames.Application.Extensions {
    public static class QueryableExtensions {
        public static IQueryable<TSource> ApplyPagination<TSource>(this IQueryable<TSource> source, int page, int limit) {
            var skip = (page - 1) * limit;
            return source.Skip(skip).Take(limit);
        }

        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, bool byDescending) {
            return byDescending ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
        }
    }
}
