using EXRGames.Domain.Contracts;
using MediatR;

namespace EXRGames.Application.Requests.Tags {
    public class DeleteTagHandler(ITagsStore store) : IRequestHandler<DeleteTagCommand> {
        public async Task Handle(DeleteTagCommand request, CancellationToken token) {
            await store.Delete(request.Tag, token);
        }
    }
}
