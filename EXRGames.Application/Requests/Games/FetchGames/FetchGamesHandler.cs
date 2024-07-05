using EXRGames.Application.Extensions;
using EXRGames.Domain;
using EXRGames.Domain.Interfaces;
using MediatR;
using System.Data.Entity;

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
            games = Sort(games, request.OrderTypes, request.OrderByDescending);
            return await games.ToListAsync(token);
        }

        private static IQueryable<Game> Filter(IQueryable<Game> games, FetchGamesQuery query) {
            games = FilterByPrice(games, query.MinPrice, query.MaxPrice);
            games = games.Include(game => game.Tags);


            if (query.Search != null) {
                games = games.Where(game => game.Title.Contains(query.Search));
            }

            if (query.Tags.Length > 0) {
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

            if (minPrice < maxPrice) {
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

        private static IQueryable<Game> Sort(IQueryable<Game> games, OrderType[] orderTypes, bool byDescending) {
            if (orderTypes.Length < 1) return games;

            foreach (var orderType in orderTypes.Distinct()) {
                switch (orderType) {
                    case OrderType.Title:
                        games = games.OrderBy(x => x.Title);
                        break;
                    case OrderType.Price:
                        games = games.OrderBy(x => x.Price);
                        break;
                }
            }

            return games;
        }
    }
}
