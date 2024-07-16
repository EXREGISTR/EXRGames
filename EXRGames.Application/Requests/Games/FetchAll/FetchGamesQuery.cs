using EXRGames.Application.Pagination;
using EXRGames.Application.Responses.Games;
using MediatR;

namespace EXRGames.Application.Requests.Games
{
    public class FetchGamesQuery : PaginableQuery, IRequest<GamesResponse> {
        public string? Search { get; set; }
        public string[]? Tags { get; set; }
        public decimal MinPrice { get; set; } = decimal.Zero;
        public decimal MaxPrice { get; set; } = decimal.MaxValue;
        public GameSortMethod[]? OrderTypes { get; set; }
    }

    public class GameSortMethod {
        public GameSortType Type { get; set; }
        public bool ByDescending { get; set; }
    }

    public enum GameSortType { Title, Price, }
}
