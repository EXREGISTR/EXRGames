using EXRGames.Domain;
using FluentValidation;

namespace EXRGames.Application.AccountRequests {
    internal class RegisterUserValidator : AbstractValidator<RegisterUserRequest> {
        public RegisterUserValidator() {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MaximumLength(UserProfile.MaxNicknameLength)
                .WithMessage($"Username exceeds the maximum allowed length: {UserProfile.MaxNicknameLength}");
        }
    }
}
