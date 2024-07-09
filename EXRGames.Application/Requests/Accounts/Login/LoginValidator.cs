using FluentValidation;

namespace EXRGames.Application.Requests.Accounts {
    public class LoginValidator : AbstractValidator<LoginCommand> {
        public LoginValidator() {
            RuleFor(x => x.Username)
                .NotEmpty();
        }
    }
}
