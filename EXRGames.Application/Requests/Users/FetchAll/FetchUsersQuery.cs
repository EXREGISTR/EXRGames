using EXRGames.Application.Extensions;
using EXRGames.Application.Pagination;
using EXRGames.Application.Responses.Users;
using EXRGames.Domain.Contracts;
using MediatR;

namespace EXRGames.Application.Requests.Users {
    public class FetchUsersQuery : PaginableQuery, IRequest<UserProfilesResponse> {
        public string? Search { get; set; }
        public int MinAge { get; set; } = 0;
        public int MaxAge { get; set; } = 120;
    }

    public class FetchUsersHandler(
        IUserProfilesStore store)
        : IRequestHandler<FetchUsersQuery, UserProfilesResponse> {
        public async Task<UserProfilesResponse> Handle(FetchUsersQuery request, CancellationToken token) {
            var users = store.All();

            var usersResponse = await users
                .Select(x => new UserProfileResponse(x.Id, x.Nickname))
                .ToPagedAsync(request.Page, request.Size);

            return new(usersResponse);
        }

        /* private static IQueryable<UserProfile> Filter(IQueryable<UserProfile> users, FetchUsersQuery request) {
             if (request.Search != null) {
                 users = users.Where(x => x.Nickname.Contains(request.Search));
             }

             if (request.MinAge > 0 || request.MaxAge < 120) {
                 users = users
                     .Where(x => x.BirthDate != null);
             }
         }*/
    }
}
