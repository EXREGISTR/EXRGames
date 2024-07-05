using Microsoft.AspNetCore.Identity;

namespace EXRGames.Application.Exceptions {
    public class RegistrationUserFailedException(IEnumerable<IdentityError> errors) : Exception {
        public IEnumerable<IdentityError> Errors { get; } = errors;
    }
}
