using EXRGames.Domain;
using System.ComponentModel.DataAnnotations;

namespace EXRGames.API.Requests.Accounts.User {
    public class RegisterUserRequest {
        [Required]
        [MaxLength(Constants.MaxNameLength)]
        public required string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public string? Nickname { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}