using EXRGames.Application.Requests.Games;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EXRGames.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController(IMediator mediator) : ControllerBase {
        private readonly IMediator mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> FetchGames([FromQuery] FetchGamesQuery query) {
            var games = await mediator.Send(query);
            return Ok(games);
        }
    }
}
