using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EXRGames.Persistense.Repositories {
    internal class UserRelationshipsStore(ApplicationContext context) : Store<UserRelationship>(context), IUserRelationshipsStore {
        public async Task<UserRelationship?> Fetch(
            string senderId, string targetId, bool includeProfiles, CancellationToken token = default) {
            var relationship = Table.Where(x => x.SenderId == senderId && x.TargetId == targetId);

            if (includeProfiles) {
                relationship.Include(x => x.SenderProifle);
                relationship.Include(x => x.TargetProfile);
            }

            return await relationship.FirstOrDefaultAsync(token);
        }
    }
}
