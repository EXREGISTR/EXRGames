using EXRGames.Domain;
using FluentValidation;

namespace EXRGames.Application.Requests.Users {
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand> {
        public RegisterUserValidator() {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MaximumLength(UserProfile.MaxNicknameLength)
                .WithMessage("Username ");
        }
    }
}
