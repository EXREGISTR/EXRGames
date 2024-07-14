using System.ComponentModel.DataAnnotations;

namespace EXRGames.API.Requests.Accounts.User {
    public class DeleteUserRequest {
        public required string Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
