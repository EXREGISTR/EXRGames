using EXRGames.Application.Pagination;
using EXRGames.Domain;

namespace EXRGames.Application.Responses.UserRelationships {
    public record FriendProfileResponse(
        string Id,
        string Nickname,
        RelationshipStatus Status
    );

    public record struct FriendProfilesResponse(PagedEnumerable<FriendProfileResponse> Profiles);
}
