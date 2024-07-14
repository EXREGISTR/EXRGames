using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using EXRGames.Domain.Extensions;
using MediatR;

namespace EXRGames.Application.Requests.UserRelationships {
    internal class SendRelationshipRequestHandler(
        IUserProfilesStore profilesStore,
        IUserRelationshipsStore friendsStore,
        IUnitOfWork unitOfWork) 
        : IRequestHandler<SendRelationshipCommand> {

        public async Task Handle(SendRelationshipCommand request, CancellationToken token) {
            var senderId = request.SenderId;
            var targetId = request.AcceptorId;

            if (senderId == targetId) return;

            if (await profilesStore.Fetch(senderId, token) == null) return;
            if (await profilesStore.Fetch(targetId, token) == null) return;

            var tempRelationship = await friendsStore.Fetch(senderId, targetId, token);
            if (tempRelationship != null) return;

            var transaction = unitOfWork.BeginTransaction();

            try {
                await CreateFriends(senderId, targetId, token);
                transaction.Commit();
            } catch (Exception) {
                transaction.Rollback();
            }
        }

        private async Task CreateFriends(string senderId, string targetId, CancellationToken token) {
            var acceptorRelationship = await friendsStore.Fetch(targetId, senderId, token);
            var acceptorSentRequest = acceptorRelationship != null;

            if (acceptorSentRequest) {
                acceptorRelationship!.Status = RelationshipStatus.Friend;
                await friendsStore.Update(acceptorRelationship, token);
            }

            var friendship = new UserRelationship {
                SenderId = senderId,
                TargetId = targetId,
                Status = acceptorSentRequest ? RelationshipStatus.Friend : null,
            };

            await friendsStore.Create(friendship, token);
        }
    }
}
