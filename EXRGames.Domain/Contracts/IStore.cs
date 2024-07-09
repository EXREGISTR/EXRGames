namespace EXRGames.Domain.Contracts {
    public interface IStore<TEntity> where TEntity : class {
        public IQueryable<TEntity> All();
        public Task Create(TEntity entity, CancellationToken token = default);
        public Task Update(TEntity entity, CancellationToken token = default);
        public Task Delete(TEntity entity, CancellationToken token = default);
    }

    public interface IStore<TEntity, TKey> : IStore<TEntity> where TEntity : class {
        public Task<TEntity?> Fetch(TKey id, CancellationToken token = default);
    }

    public interface IStore<TEntity, TKey1, TKey2> : IStore<TEntity> where TEntity : class {
        public Task<TEntity?> Fetch(TKey1 firstId, TKey2 secondId, CancellationToken token = default);
    }
}
