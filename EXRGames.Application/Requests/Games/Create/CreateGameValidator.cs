using EXRGames.Domain;
using FluentValidation;

namespace EXRGames.Application.Requests.Games {
    public class CreateGameValidator : AbstractValidator<CreateGameCommand> {
        public CreateGameValidator() {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(Game.MaxTitleLength);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);
        }
    }
}
