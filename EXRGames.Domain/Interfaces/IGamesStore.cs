namespace EXRGames.Domain.Interfaces {
    public interface IGamesStore {
        public Task Add(Game game, CancellationToken token = default);
        public Task Update(Game game, CancellationToken token = default);
        public IQueryable<Game> FetchAll();
        public Task<Game?> Fetch(Guid id, CancellationToken token = default);
        public Task DeleteGame(Guid id);
    }
}
