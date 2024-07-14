namespace EXRGames.Domain.Contracts {
    public interface IUserRelationshipsStore : IStore<UserRelationship> {
        public Task<UserRelationship?> Fetch(
            string senderId, string targetId, bool includeProfiles, CancellationToken token = default);
    }
}
