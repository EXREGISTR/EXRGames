using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using MediatR;

namespace EXRGames.Application.Requests.Games {
    public class FetchGameHandler(IGamesStore store) : IRequestHandler<FetchGameQuery, Game> {
        public async Task<Game> Handle(FetchGameQuery request, CancellationToken token) {
            var game = await store.Fetch(request.Id, token) 
                ?? throw new NullReferenceException();

            return game;
        }
    }
}
