using EXRGames.Application.Exceptions;
using EXRGames.Domain;
using EXRGames.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EXRGames.Application.Requests.Users {
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, string> {
        private readonly IUserProfilesStore profilesStore;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public RegisterUserHandler(
            IUserProfilesStore profilesStore,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager) {
            this.profilesStore = profilesStore;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken token) {
            var tempUser = await userManager.FindByNameAsync(request.Username);
            if (tempUser != null) {
                throw new UserAlreadyRegisteredException(request.Username);
            }

            var user = new IdentityUser {
                UserName = request.Username,
            };

            var profile = new UserProfile {
                Nickname = request.Nickname ?? request.Username,
                RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow),
                BirthDate = request.BirthDate,
            };

            await profilesStore.Add(profile, token);

            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) {
                throw new RegistrationUserFailedException(result.Errors);
            }

            await signInManager.SignInAsync(user, isPersistent: false);
            return user.Id;
        }
    }
}
