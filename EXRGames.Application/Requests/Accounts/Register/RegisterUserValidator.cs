using EXRGames.Domain;
using FluentValidation;

namespace EXRGames.Application.Requests.Accounts {
    internal class RegisterUserValidator : AbstractValidator<RegisterUserCommand> {
        public RegisterUserValidator() {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MaximumLength(UserProfile.MaxNicknameLength)
                .WithMessage($"Username exceeds the maximum allowed length: {UserProfile.MaxNicknameLength}");
        }
    }
}
