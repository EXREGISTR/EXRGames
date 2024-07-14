using EXRGames.Domain.Contracts;
using EXRGames.Application.Exceptions;
using MediatR;

namespace EXRGames.Application.Requests.Tags {
    public class CreateTagHandler(ITagsStore store) : IRequestHandler<CreateTagCommand> {
        private readonly ITagsStore store = store;

        public async Task Handle(CreateTagCommand request, CancellationToken token) {
            if (await store.Exists(request.Tag, token)) {
                throw new EntityAlreadyExistsException();
            }

            await store.Create(request.Tag.ToLower(), token);
        }
    }
}
