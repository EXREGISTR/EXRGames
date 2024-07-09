using MediatR;

namespace EXRGames.Application.Requests.Accounts {
    public class LoginCommand : IRequest<string> {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
