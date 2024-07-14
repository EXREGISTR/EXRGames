namespace EXRGames.Application.Responses.Games {
    public record GameResponse(
        Guid Id, 
        string Title,
        decimal Price
    );

    public record struct GamesResponse(IEnumerable<GameResponse> Games);
}
