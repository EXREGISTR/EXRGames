using EXRGames.Application.Exceptions.Account;
using EXRGames.Domain.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace EXRGames.Application.Requests.Accounts {
    internal class DeleteUserHandler(UserManager<IdentityUser> userManager, IUserProfilesStore profilesStore) : IRequestHandler<DeleteUserCommand> {
        public async Task Handle(DeleteUserCommand request, CancellationToken token) {
            var user = await userManager.FindByIdAsync(request.Id) 
                ?? throw new UserCouldntBeDeletedException("User doesn't exists");

            var result = await userManager.CheckPasswordAsync(user, request.Password);
            if (!result) {
                throw new UserCouldntBeDeletedException("Invalid password");
            }

            using var transactionScope = new TransactionScope();

            await userManager.DeleteAsync(user);

            var profile = await profilesStore.Fetch(request.Id, token)
                ?? throw new UserCouldntBeDeletedException("Profile doesn't exists. How do you do this???");

            await profilesStore.Delete(profile!, token);

            transactionScope.Complete();
        }
    }
}
