using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EXRGames.Persistense.Repositories {
    internal class UserProfilesStore(ApplicationContext context) : Store<UserProfile>(context), IUserProfilesStore {
        public async Task<UserProfile?> Fetch(string id, CancellationToken token = default)
            => await Table.FirstOrDefaultAsync(x => x.Id == id, token);
    }
}
