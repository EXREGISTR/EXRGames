namespace EXRGames.Domain.Contracts {
    public interface ITagsStore {
        public Task Create(string name, CancellationToken token = default);
        public Task<bool> Exists(string name, CancellationToken token = default);
        public Task Delete(string name, CancellationToken token = default);
        public IQueryable<Tag> All();
    }
}
