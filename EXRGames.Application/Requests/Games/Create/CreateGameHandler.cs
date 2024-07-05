using EXRGames.Domain;
using EXRGames.Domain.Interfaces;
using EXRGames.Application.Extensions;
using MediatR;

namespace EXRGames.Application.Requests.Games {
    public class CreateGameHandler : IRequestHandler<CreateGameCommand, string> { 
        private readonly IGamesStore gamesStore;
        private readonly ITagsStore tagsStore;

        public CreateGameHandler(IGamesStore gamesStore, ITagsStore tagsStore) {
            this.gamesStore = gamesStore;
            this.tagsStore = tagsStore;
        }

        public async Task<string> Handle(CreateGameCommand request, CancellationToken token) {
            var tags = await tagsStore.FetchAll()
                .Where(tag => request.Tags.Contains(tag.Name))
                .ToListAsync(token);
            
            var game = new Game { 
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Tags = tags,
            };

            await gamesStore.Add(game, token);

            return game.Id.ToString();
        }
    }
}
