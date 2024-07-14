using EXRGames.Application.Responses.UserRelationships;
using EXRGames.Domain;
using MediatR;

namespace EXRGames.Application.Requests.UserRelationships {
    public class FetchFriendsQuery : PaginableQuery, IRequest<FriendProfilesResponse> {
        public required string UserId { get; set; }
        public RelationshipStatus? Status { get; set; }
    }
}
