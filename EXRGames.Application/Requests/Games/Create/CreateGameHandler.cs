using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EXRGames.Application.Requests.Games {
    internal class CreateGameHandler(IGamesStore gamesStore, ITagsStore tagsStore) : IRequestHandler<CreateGameCommand, string> {
        public async Task<string> Handle(CreateGameCommand request, CancellationToken token) {
            ICollection<Tag> tags = [];

            if (request.Tags != null) {
                tags = await tagsStore.All()
                    .Where(tag => request.Tags.Contains(tag.Name))
                    .ToListAsync(token);
            }
            
            var game = new Game { 
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Tags = tags,
            };

            await gamesStore.Create(game, token);

            return game.Id.ToString();
        }
    }
}
