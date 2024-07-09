using MediatR;

namespace EXRGames.Application.Requests.Tags {
    public class FetchTagsQuery : IRequest<IEnumerable<string>>;
}
