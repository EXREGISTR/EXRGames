namespace EXRGames.Domain.Contracts {
    public interface IUserProfilesStore : IStore<UserProfile> {
        public Task<UserProfile?> Fetch(string id, bool includeFriends, CancellationToken token = default);
    }
}
