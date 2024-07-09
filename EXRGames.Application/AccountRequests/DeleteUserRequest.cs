namespace EXRGames.Application.AccountRequests {
    public class DeleteUserRequest {
        public required string Id { get; set; }
        public required string Password { get; set; }
    }
}
