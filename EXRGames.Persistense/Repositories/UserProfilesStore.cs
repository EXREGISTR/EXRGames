using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EXRGames.Persistense.Repositories {
    internal class UserProfilesStore(ApplicationContext context) : Store<UserProfile>(context), IUserProfilesStore {
        public async Task<UserProfile?> Fetch(string id, bool includeFriends, CancellationToken token = default) {
            var profile = Table.Where(x => x.Id == id);
            if (includeFriends) {
                profile = profile.Include(x => x.Friends);
            }

            return await profile.FirstOrDefaultAsync(token);
        }
    }
}
