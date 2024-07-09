using EXRGames.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EXRGames.Persistense.Repositories {
    internal abstract class Store<T>(ApplicationContext context) : IStore<T> where T : class {
        protected DbSet<T> Table => context.Set<T>();

        public IQueryable<T> All() => Table.AsNoTracking();

        public async Task Create(T entity, CancellationToken token = default) {
            await Table.AddAsync(entity, token);
            await context.SaveChangesAsync(token);
        }

        public async Task Update(T entity, CancellationToken token = default) {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync(token);
        }

        public async Task Delete(T entity, CancellationToken token = default) {
            Table.Remove(entity);
            await context.SaveChangesAsync(token);
        }
    }
}
