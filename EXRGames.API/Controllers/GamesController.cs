using EXRGames.Application.Requests.Games;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXRGames.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController(IMediator mediator) : ControllerBase {
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameCommand request, CancellationToken token) {
            var gameId = await mediator.Send(request, token);
            return Ok(gameId);
        }

        [HttpGet]
        public async Task<IActionResult> FetchGames([FromQuery] FetchGamesQuery request, CancellationToken token) {
            var games = await mediator.Send(request, token);
            return Ok(games);
        }

        [HttpGet("find")]
        public async Task<IActionResult> FetchGames([FromQuery] FetchGameQuery request, CancellationToken token) {
            var games = await mediator.Send(request, token);
            return Ok(games);
        }
    }
}
