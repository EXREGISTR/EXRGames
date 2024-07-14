using EXRGames.Domain;
using System.ComponentModel.DataAnnotations;

namespace EXRGames.API.Requests.Accounts.User {
    public class LoginRequest {
        [Required]
        [MaxLength(Constants.MaxNameLength)]
        public required string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
