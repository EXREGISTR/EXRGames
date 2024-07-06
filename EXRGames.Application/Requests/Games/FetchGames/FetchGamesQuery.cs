using EXRGames.Domain;
using MediatR;

namespace EXRGames.Application.Requests.Games {
    public class FetchGamesQuery : PaginableQuery, IRequest<IEnumerable<Game>> {
        public string? Search { get; set; }
        public string[]? Tags { get; set; }
        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = decimal.MaxValue;
        public GameSortMethod[]? OrderTypes { get; set; }
    }

    public class GameSortMethod {
        public GameSortType Type { get; set; }
        public bool ByDescending { get; set; }
    }

    public enum GameSortType { Title, Price, }
}
