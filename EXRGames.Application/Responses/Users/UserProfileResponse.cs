using EXRGames.Application.Pagination;

namespace EXRGames.Application.Responses.Users {
    public record UserProfileResponse(
        string Id,
        string Nickname
    );

    public record struct UserProfilesResponse(
        PagedEnumerable<UserProfileResponse> Profiles
    );
}
