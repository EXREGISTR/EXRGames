using EXRGames.Application.Extensions;
using EXRGames.Application.Exceptions;
using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EXRGames.Application.Responses.Games;

namespace EXRGames.Application.Requests.Games {
    public class FetchGamesHandler(IGamesStore gamesStore) 
        : IRequestHandler<FetchGamesQuery, GamesResponse> {
        public async Task<GamesResponse> Handle(FetchGamesQuery request, CancellationToken token) {
            var games = gamesStore.All();

            games = Filter(games, request);
            games = Sort(games, request);

            var gamesResponse = await games
                .Select(x => new GameResponse(x.Id, x.Title, x.Price))
                .ToPagedAsync(request.Page, request.Size, token);

            return new(gamesResponse);
        }

        private static IQueryable<Game> Filter(IQueryable<Game> games, FetchGamesQuery query) {
            games = FilterByPrice(games, query.MinPrice, query.MaxPrice);

            if (query.Search != null) {
                games = games.Where(game => game.Title.Contains(query.Search));
            }

            if (query.Tags != null) {
                games = games
                    .Where(game => game.Tags.Any(tag => query.Tags.Contains(tag.Name)));
            }

            return games;
        }

        private static IQueryable<Game> FilterByPrice(IQueryable<Game> games, decimal minPrice, decimal maxPrice) {
            if (maxPrice == decimal.Zero) {
                return games.Where(x => x.Price == decimal.Zero);
            }

            if (minPrice == maxPrice) {
                return games.Where(game => game.Price == maxPrice);
            }

            if (minPrice > maxPrice) {
                minPrice = maxPrice;
                maxPrice = minPrice;
            }

            if (minPrice > decimal.Zero) {
                games = games.Where(x => x.Price >= minPrice);
            }

            if (maxPrice < decimal.MaxValue) {
                games = games.Where(x => x.Price <= maxPrice);
            }

            return games;
        }

        private static IQueryable<Game> Sort(IQueryable<Game> games, FetchGamesQuery query) {
            if (query.OrderTypes == null) return games;

            foreach (var orderMethod in query.OrderTypes.Distinct()) {
                var orderSelector = GetOrderSelector(orderMethod.Type);
                games = games.OrderBy(orderSelector, orderMethod.ByDescending);
            }

            return games;
        }

        private static Expression<Func<Game, object>> GetOrderSelector(GameSortType type) {
            return type switch {
                GameSortType.Title => game => game.Title,
                GameSortType.Price => game => game.Price,
                _ => throw new UnknownSortTypeException()
            };
        }
    }
}
