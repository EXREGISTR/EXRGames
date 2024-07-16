using EXRGames.Application.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EXRGames.Application.Extensions {
    public static class QueryableExtensions {
        public static async Task<PagedEnumerable<TSource>> ToPagedAsync<TSource>(
            this IQueryable<TSource> source, int page, int size, CancellationToken token = default) {
            var skip = (page - 1) * size;

            var enumerable = await source
                .Skip(skip)
                .Take(size)
                .ToListAsync(token);

            var totalCount = await source.CountAsync(token);

            return new PagedEnumerable<TSource>(enumerable, page, size, totalCount);
        }

        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>
            (this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, bool byDescending)
            => byDescending ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
    }
}
