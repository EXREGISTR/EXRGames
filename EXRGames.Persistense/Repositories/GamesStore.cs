using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EXRGames.Persistense.Repositories {
    internal class GamesStore(ApplicationContext context) : Store<Game>(context), IGamesStore {
        public async Task<Game?> Fetch(Guid id, CancellationToken token) {
            return await All()
                .Where(x => x.Id == id)
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id, token);
        }
    }
}
