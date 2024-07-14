using MediatR;

namespace EXRGames.Application.Requests.UserRelationships {
    public class SendRelationshipCommand : IRequest {
        public required string SenderId { get; set; }
        public required string AcceptorId { get; set; }
    }
}