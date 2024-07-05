namespace EXRGames.Domain.Interfaces {
    public interface ITagsStore {
        public Task Add(string name, CancellationToken token = default);
        public Task Delete(string name, CancellationToken token = default);
        public IQueryable<Tag> FetchAll();
    }
}
