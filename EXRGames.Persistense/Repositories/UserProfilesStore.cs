using EXRGames.Domain;
using EXRGames.Domain.Interfaces;

namespace EXRGames.Persistense.Repositories {
    public class UserProfilesStore : IUserProfilesStore {
        public Task Add(UserProfile profile, CancellationToken token = default) {
            throw new NotImplementedException();
        }

        public IQueryable<UserProfile> FetchAll() {
            throw new NotImplementedException();
        }

        public Task Update(UserProfile profile, CancellationToken token = default) {
            throw new NotImplementedException();
        }
    }
}
