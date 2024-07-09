using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EXRGames.Application.Requests.Accounts {
    public class LogoutHandler(SignInManager<IdentityUser> signInManager) : IRequestHandler<LogoutCommand> {
        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken) {
            await signInManager.SignOutAsync();
        }
    }
}
