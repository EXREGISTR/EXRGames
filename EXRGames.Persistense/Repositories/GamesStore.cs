using EXRGames.Domain;
using EXRGames.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EXRGames.Persistense.Repositories {
    public class GamesStore(ApplicationContext context) : IGamesStore {
        private readonly ApplicationContext context = context;

        public Task Add(Game game, CancellationToken token = default) {
            throw new NotImplementedException();
        }

        public Task<Game> Fetch(Guid id, CancellationToken token = default) {
            throw new NotImplementedException();
        }

        public IQueryable<Game> FetchAll()
            => context.Games.AsNoTracking();

        public Task Update(Game game, CancellationToken token = default) {

        }
    }
}
