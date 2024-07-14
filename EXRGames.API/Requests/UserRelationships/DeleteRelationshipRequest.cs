using System.ComponentModel.DataAnnotations;

namespace EXRGames.API.Requests.UserRelationships {
    public class DeleteRelationshipRequest {
        [Required]
        public required string AcceptorId { get; set; }
    }
}