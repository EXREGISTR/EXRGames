using EXRGames.Domain;
using System.ComponentModel.DataAnnotations;

namespace EXRGames.API.Requests.UserRelationships {
    public class UpdateRelationshipStatusRequest {
        [Required]
        public required string AcceptorId { get; set; }

        [Required]
        public required RelationshipStatus Status { get; set; }
    }
}