namespace EXRGames.Application.AccountRequests {
    public class RegisterUserRequest {
        public required string Username { get; set; }
        public required string Password { get; set; }

        public string? Nickname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}