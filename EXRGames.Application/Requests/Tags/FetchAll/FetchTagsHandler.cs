using EXRGames.Domain.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EXRGames.Application.Requests.Tags {
    public class FetchTagsHandler(ITagsStore store) : IRequestHandler<FetchTagsQuery, IEnumerable<string>> {
        private readonly ITagsStore store = store;

        public async Task<IEnumerable<string>> Handle(FetchTagsQuery request, CancellationToken token) {
            var tags = await store.All().Select(x => x.Name).ToListAsync(token);
            return tags;
        }
    }
}
