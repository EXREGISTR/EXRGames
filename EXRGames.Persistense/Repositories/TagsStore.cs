using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EXRGames.Persistense.Repositories {
    internal class TagsStore(ApplicationContext context) : ITagsStore {
        private readonly ApplicationContext context = context;

        public async Task Create(string name, CancellationToken token) {
            await context.Tags.AddAsync(new Tag { Name = name }, token);
            await context.SaveChangesAsync(token);
        }

        public async Task<bool> Exists(string name, CancellationToken token) 
            => await All().ContainsAsync(new Tag { Name = name }, token);

        public async Task Delete(string name, CancellationToken token) 
            => await context.Tags.Where(x => x.Name == name).ExecuteDeleteAsync(token);

        public IQueryable<Tag> All() => context.Tags.AsNoTracking();
    }
}
