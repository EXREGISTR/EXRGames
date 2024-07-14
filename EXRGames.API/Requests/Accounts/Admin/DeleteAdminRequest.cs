using EXRGames.Domain;
using System.ComponentModel.DataAnnotations;

namespace EXRGames.API.Requests.Accounts.Admin {
    public class DeleteAdminRequest {
        [Required]
        [MaxLength(Constants.MaxNameLength)]
        public required string Username { get; set; }
    }
}
