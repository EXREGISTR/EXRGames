using EXRGames.Domain;
using MediatR;

namespace EXRGames.Application.Requests.Games {
    public class FetchGameQuery : IRequest<Game> {
        public Guid Id { get; set; }
    }
}
