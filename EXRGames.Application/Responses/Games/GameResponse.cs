using EXRGames.Application.Pagination;

namespace EXRGames.Application.Responses.Games {
    public record GameResponse(
        Guid Id, 
        string Title,
        decimal Price
    );

    public record struct GamesResponse(PagedEnumerable<GameResponse> Games);
}
