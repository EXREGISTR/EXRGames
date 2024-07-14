using EXRGames.Domain.Contracts;

namespace EXRGames.Domain.Extensions {
    public static class UserRelationshipsStoreExtensions {
        public static Task<UserRelationship?> Fetch(this IUserRelationshipsStore store,
            string senderId, string targetId, CancellationToken token) => 
            store.Fetch(senderId, targetId, false, token);

        public static Task<UserRelationship?> FetchIncludeProfiles(this IUserRelationshipsStore store,
            string senderId, string targetId, CancellationToken token) =>
            store.Fetch(senderId, targetId, true, token);
    }
}
