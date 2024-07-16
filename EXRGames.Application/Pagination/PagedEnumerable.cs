namespace EXRGames.Application.Pagination {
    public readonly record struct PagedEnumerable<T>(IEnumerable<T> Objects, int Page, int Size, int TotalCount);
}
