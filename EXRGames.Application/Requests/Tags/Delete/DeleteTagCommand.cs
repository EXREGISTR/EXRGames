using MediatR;

namespace EXRGames.Application.Requests.Tags {
    public class DeleteTagCommand : IRequest {
        public required string Tag { get; set; }
    }
}
