using EXRGames.Application.Exceptions.Account;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EXRGames.Application.Requests.Accounts {
    public class LoginCommandHandler(
        SignInManager<IdentityUser> signInManager, 
        UserManager<IdentityUser> userManager) 
        : IRequestHandler<LoginCommand, string> {
        public async Task<string> Handle(LoginCommand request, CancellationToken token) {
            var result = await signInManager.PasswordSignInAsync(
                request.Username, request.Password, request.RememberMe, false);

            if (!result.Succeeded) {
                throw new LoginFailedException();
            }


            var user = await userManager.FindByNameAsync(request.Username);
            return user!.Id;
        }
    }
}
