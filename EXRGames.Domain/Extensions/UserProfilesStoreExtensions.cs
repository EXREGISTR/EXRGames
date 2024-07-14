using EXRGames.Domain.Contracts;

namespace EXRGames.Domain.Extensions {
    public static class UserProfilesStoreExtensions {
        public static Task<UserProfile?> Fetch(this IUserProfilesStore store, 
            string id, CancellationToken token) => store.Fetch(id, false, token);

        public static Task<UserProfile?> FetchIncludeFriends(this IUserProfilesStore store,
            string id, CancellationToken token) => store.Fetch(id, true, token);
    }
}
