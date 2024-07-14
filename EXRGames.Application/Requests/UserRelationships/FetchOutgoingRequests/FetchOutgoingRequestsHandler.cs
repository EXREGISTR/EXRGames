using EXRGames.Domain.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EXRGames.Application.Extensions;
using EXRGames.Application.Responses.Users;

namespace EXRGames.Application.Requests.UserRelationships
{
    internal class FetchOutgoingRequestsHandler(IUserRelationshipsStore store) 
        : IRequestHandler<FetchOutgoingRequestsQuery, UserProfilesResponse> {
        public async Task<UserProfilesResponse> Handle(FetchOutgoingRequestsQuery request, CancellationToken token) {
            var profiles = await store.All()
                .ApplyPagination(request.Page, request.Limit)
                .Where(x => x.SenderId == request.UserId && x.Status == null)
                .Select(x => new UserProfileResponse(
                    x.TargetProfile.Id,
                    x.TargetProfile.Nickname))
                .ToListAsync(token);

            return new(profiles);
        }
    }
}
