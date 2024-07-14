using EXRGames.Domain;
using MediatR;

namespace EXRGames.Application.Requests.UserRelationships {
    public class UpdateRelationshipStatusCommand : IRequest {
        public required string SenderId { get; set; } 
        public required string AcceptorId { get; set; }
        public RelationshipStatus Status { get; set; }
    }
}
