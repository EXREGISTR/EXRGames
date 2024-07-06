using EXRGames.Domain;
using EXRGames.Domain.Interfaces;

namespace EXRGames.Persistense.Repositories {
    public class TagsStore : ITagsStore {
        public Task Add(string name, CancellationToken token = default) {
            throw new NotImplementedException();
        }

        public Task Delete(string name, CancellationToken token = default) {
            throw new NotImplementedException();
        }

        public IQueryable<Tag> FetchAll() {
            throw new NotImplementedException();
        }
    }
}
