using MediatR;

namespace EXRGames.Application.Requests.Users {
    public class RegisterUserCommand : IRequest<string> {
        public required string Username { get; set; }
        public required string Password { get; set; }

        public string? Nickname { get; set; }
        public DateOnly? BirthDate { get; set; }
    }
}
