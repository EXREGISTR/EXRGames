namespace EXRGames.Domain.Contracts {
    public interface IStore<TEntity> where TEntity : class {
        public IQueryable<TEntity> All();
        public Task Create(TEntity entity, CancellationToken token = default);
        public Task Update(TEntity entity, CancellationToken token = default);
        public Task Delete(TEntity entity, CancellationToken token = default);
    }
}
