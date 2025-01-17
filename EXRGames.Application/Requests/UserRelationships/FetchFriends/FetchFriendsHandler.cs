﻿using EXRGames.Application.Extensions;
using EXRGames.Application.Responses.UserRelationships;
using EXRGames.Domain.Contracts;
using MediatR;

namespace EXRGames.Application.Requests.UserRelationships {
    internal class FetchFriendsHandler(IUserRelationshipsStore store) : IRequestHandler<FetchFriendsQuery, FriendProfilesResponse> {
        public async Task<FriendProfilesResponse> Handle(FetchFriendsQuery request, CancellationToken token) {
            var relationships = store.All();

            if (request.Status != null) {
                relationships = relationships
                    .Where(x => x.SenderId == request.UserId && x.Status == request.Status);
            } else {
                relationships = relationships
                    .Where(x => x.SenderId == request.UserId && x.Status != null);
            }

            var profiles = await relationships
                .Select(x => new FriendProfileResponse(
                    x.TargetProfile.Id, 
                    x.TargetProfile.Nickname,
                    x.Status!.Value))
                .ToPagedAsync(request.Page, request.Size, token);

            return new(profiles);
        }
    }
}
