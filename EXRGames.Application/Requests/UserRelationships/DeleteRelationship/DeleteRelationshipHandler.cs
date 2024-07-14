using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using EXRGames.Domain.Extensions;
using MediatR;

namespace EXRGames.Application.Requests.UserRelationships {
    internal class DeleteRelationshipHandler(
        IUserRelationshipsStore store,
        IUnitOfWork unitOfWork) 
        : IRequestHandler<DeleteRelationshipCommand> {
        public async Task Handle(DeleteRelationshipCommand request, CancellationToken token) {
            var relationship = await store.Fetch(request.SenderId, request.AcceptorId, token);
            if (relationship == null) return;

            var transaction = unitOfWork.BeginTransaction();
            try {
                await DeleteRelationship(relationship, request, token);
                transaction.Commit();
            } catch (Exception) {
                transaction.Rollback();
            }
        }

        private async Task DeleteRelationship(UserRelationship relationship, DeleteRelationshipCommand request, CancellationToken token) {
            await store.Delete(relationship, token);

            var acceptorRelationship = await store.Fetch(request.AcceptorId, request.SenderId, token);
            if (acceptorRelationship == null) return;

            acceptorRelationship.Status = null;
            await store.Update(acceptorRelationship, token);
        }
    }
}
