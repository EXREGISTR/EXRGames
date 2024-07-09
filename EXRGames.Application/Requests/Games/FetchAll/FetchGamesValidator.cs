using FluentValidation;

namespace EXRGames.Application.Requests.Games {
    public class FetchGamesValidator : AbstractValidator<FetchGamesQuery> {
        public FetchGamesValidator() {
            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Incorrect minimal price value");

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Incorrect maximal price value");
        }
    }
}
