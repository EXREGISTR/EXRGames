using EXRGames.Application.Responses.Users;
using MediatR;

namespace EXRGames.Application.Requests.UserRelationships
{
    public class FetchOutgoingRequestsQuery : PaginableQuery, IRequest<UserProfilesResponse> {
        public required string UserId { get; set; }
    }
}
