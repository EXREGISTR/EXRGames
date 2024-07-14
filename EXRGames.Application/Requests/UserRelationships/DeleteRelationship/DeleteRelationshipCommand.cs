using MediatR;

namespace EXRGames.Application.Requests.UserRelationships {
    public class DeleteRelationshipCommand : IRequest {
        public required string SenderId { get; set; }
        public required string AcceptorId { get; set; }
    }
}
