using MediatR;

namespace EXRGames.Application.Requests.Games {
    public class CreateGameCommand : IRequest<string> {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required ICollection<string> Tags { get; set; }
    }
}
