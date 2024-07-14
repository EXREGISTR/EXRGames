namespace EXRGames.Application.Responses.Users {
    public record UserProfileResponse(
        string Id,
        string Nickname
    );

    public record struct UserProfilesResponse(
        IEnumerable<UserProfileResponse> Profiles
    );
}
