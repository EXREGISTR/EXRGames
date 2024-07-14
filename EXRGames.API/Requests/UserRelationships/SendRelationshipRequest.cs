using System.ComponentModel.DataAnnotations;

namespace EXRGames.API.Requests.UserRelationships {
    public class SendRelationshipRequest {
        [Required]
        public required string AcceptorId { get; set; }
    }
}