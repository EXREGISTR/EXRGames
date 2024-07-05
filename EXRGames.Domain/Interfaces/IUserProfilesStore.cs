namespace EXRGames.Domain.Interfaces {
    public interface IUserProfilesStore {
        public Task Add(UserProfile profile, CancellationToken token = default);
        public Task Update(UserProfile profile, CancellationToken token = default);
        public IQueryable<UserProfile> FetchAll();
    }
}
