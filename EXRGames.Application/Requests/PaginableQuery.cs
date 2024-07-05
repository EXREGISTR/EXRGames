namespace EXRGames.Application.Requests {
    public abstract class PaginableQuery {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = int.MaxValue;
    }
}
