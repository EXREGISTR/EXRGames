using EXRGames.Application.Extensions;
using EXRGames.Application.Responses.Users;
using EXRGames.Domain.Contracts;
using MediatR;

namespace EXRGames.Application.Requests.UserRelationships {
    internal class FetchOutgoingRequestsHandler(IUserRelationshipsStore store)
        : IRequestHandler<FetchOutgoingRequestsQuery, UserProfilesResponse> {
        public async Task<UserProfilesResponse> Handle(FetchOutgoingRequestsQuery request, CancellationToken token) {
            var profiles = await store.All()
                .Where(x => x.SenderId == request.UserId && x.Status == null)
                .Select(x => new UserProfileResponse(
                    x.TargetProfile.Id,
                    x.TargetProfile.Nickname))
                .ToPagedAsync(request.Page, request.Size, token);

            return new(profiles);
        }
    }
}
