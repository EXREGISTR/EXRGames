using EXRGames.Domain;
using EXRGames.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EXRGames.Persistense.Repositories {
    public class GamesStore(ApplicationContext context) : IGamesStore {
        private readonly ApplicationContext context = context;

        public async Task Add(Game game, CancellationToken token = default) {
            await context.AddAsync(game, token);
            await context.SaveChangesAsync(token);
        }

        public async Task<Game?> Fetch(Guid id, CancellationToken token = default) {
            return await FetchAll().FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public IQueryable<Game> FetchAll()
            => context.Games.AsNoTracking();

        public async Task Update(Game game, CancellationToken token = default) {
            await context.Games.Where(x => x.Id == game.Id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.Title, game.Title)
                .SetProperty(x => x.Description, game.Description)
                .SetProperty(x => x.Price, game.Price)
                .SetProperty(x => x.Tags, game.Tags)
                , token);
        }

        public async Task DeleteGame(Guid id) {
            await context.Games.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}
