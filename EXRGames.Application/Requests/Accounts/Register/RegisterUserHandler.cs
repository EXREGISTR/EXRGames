using EXRGames.Application.Exceptions.Account;
using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EXRGames.Application.Requests.Accounts {
    internal class RegisterUserHandler(
        IUserProfilesStore profilesStore,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager) 
        : IRequestHandler<RegisterUserCommand, string> {

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken token) {
            var tempUser = await userManager.FindByNameAsync(request.Username);
            if (tempUser != null) {
                throw new UserAlreadyRegisteredException();
            }

            var id = Guid.NewGuid().ToString();

            var user = new IdentityUser {
                Id = id,
                UserName = request.Username,
            };

            var profile = new UserProfile {
                Id = id,
                Nickname = request.Nickname ?? request.Username,
                RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow),
                BirthDate = DateOnly.FromDateTime(request.BirthDate),
            };

            await profilesStore.Create(profile, token);

            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) {
                throw new RegistrationFailedException(result.Errors);
            }

            await signInManager.SignInAsync(user, isPersistent: false);
            return user.Id;
        }
    }
}
