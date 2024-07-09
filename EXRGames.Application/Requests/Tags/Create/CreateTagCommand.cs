using MediatR;

namespace EXRGames.Application.Requests.Tags {
    public class CreateTagCommand : IRequest {
        public required string Tag { get; set; }
    }
}
