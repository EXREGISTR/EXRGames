namespace EXRGames.Domain.Contracts {
    public interface IGamesStore : IStore<Game> {
        public Task<Game?> Fetch(Guid id, CancellationToken token = default);
    }
}
