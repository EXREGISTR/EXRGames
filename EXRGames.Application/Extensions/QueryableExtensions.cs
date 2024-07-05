namespace EXRGames.Application.Extensions {
    public static class QueryableExtensions {
        public static IQueryable<TSource> ApplyPagination<TSource>(this IQueryable<TSource> source, int page, int limit) {
            var skip = (page - 1) * limit;
            return source.Skip(skip).Take(limit);
        }
    }
}
