using FluentValidation;

namespace EXRGames.Application.Requests.Tags {
    public class CreateTagValidator : AbstractValidator<CreateTagCommand> {
        public CreateTagValidator() {
            RuleFor(x => x.Tag)
                .NotEmpty()
                .WithMessage("Invalid tag name!");
        }
    }
}
