using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EXRGames.Persistense.Repositories {
    internal class FriendsStore(ApplicationContext context) : Store<Friend>(context), IFriendsStore {
        public async Task<Friend?> Fetch(string firstId, string secondId, CancellationToken token = default) 
            => await Table.FirstOrDefaultAsync(x => x.SenderId == firstId && x.TargetId == secondId, token);
    }
}
