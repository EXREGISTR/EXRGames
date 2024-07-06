using EXRGames.Application.Extensions;
using EXRGames.Domain;
using EXRGames.Domain.Interfaces;
using MediatR;
using System.Data.Entity;
using System.Linq.Expressions;

namespace EXRGames.Application.Requests.Games {
    public class FetchGamesHandler : IRequestHandler<FetchGamesQuery, IEnumerable<Game>> {
        private readonly IGamesStore gamesStore;

        public FetchGamesHandler(IGamesStore gamesStore) {
            this.gamesStore = gamesStore;
        }

        public async Task<IEnumerable<Game>> Handle(FetchGamesQuery request, CancellationToken token) {
            if (request.MaxPrice == 0) return [];

            var games = gamesStore.FetchAll().ApplyPagination(request.Page, request.Limit);
            games = Filter(games, request);
            games = Sort(games, request);
            return await games.ToListAsync(token);
        }

        private static IQueryable<Game> Filter(IQueryable<Game> games, FetchGamesQuery query) {
            games = FilterByPrice(games, query.MinPrice, query.MaxPrice);
            games = games.Include(game => game.Tags);


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
            if (minPrice == maxPrice) {
                games = games.Where(game => game.Price == maxPrice);
                return games;
            }

            if (minPrice > maxPrice) {
                minPrice = maxPrice;
                maxPrice = minPrice;
            }

            if (minPrice > 0) {
                games = games.Where(game => game.Price >= minPrice);
            }

            if (maxPrice < int.MaxValue) {
                games = games.Where(game => game.Price <= maxPrice);
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
                _ => throw new ArgumentOutOfRangeException(nameof(type)),
            };
        }
    }
}
