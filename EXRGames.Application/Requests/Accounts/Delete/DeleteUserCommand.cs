using MediatR;

namespace EXRGames.Application.Requests.Accounts {
    public class DeleteUserCommand : IRequest {
        public required string Id { get; set; }
        public required string Password { get; set; }
    }
}
