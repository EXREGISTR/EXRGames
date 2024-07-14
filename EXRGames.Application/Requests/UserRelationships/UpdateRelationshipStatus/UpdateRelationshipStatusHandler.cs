using EXRGames.Domain.Contracts;
using EXRGames.Domain.Extensions;
using MediatR;

namespace EXRGames.Application.Requests.UserRelationships {
    internal class UpdateRelationshipStatusHandler(IUserRelationshipsStore store) : IRequestHandler<UpdateRelationshipStatusCommand> {
        public async Task Handle(UpdateRelationshipStatusCommand request, CancellationToken token) {
            var friendship = await store.Fetch(request.SenderId, request.AcceptorId, token);
            if (friendship == null || friendship.Status == null) return;

            friendship.Status = request.Status;
            await store.Update(friendship, token);
        }
    }
}
