using Microsoft.AspNetCore.Identity;

namespace EXRGames.Application.Exceptions.Account {
    public class RegistrationFailedException(IEnumerable<IdentityError> errors) : Exception {
        public IEnumerable<IdentityError> Errors { get; } = errors;
    }
}
