using EXRGames.Domain;
using MediatR;

namespace EXRGames.Application.Requests.Games {
    public class FetchGamesQuery : PaginableQuery, IRequest<IEnumerable<Game>> {
        public string? Search { get; set; }
        public string[] Tags { get; set; } = [];
        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = decimal.MaxValue;
        public bool OrderByDescending { get; set; }
        public OrderType[] OrderTypes { get; set; } = [];
    }

    public enum OrderType { Title, Price, }
}
