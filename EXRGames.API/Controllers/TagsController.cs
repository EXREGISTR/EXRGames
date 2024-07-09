using EXRGames.API.Constants;
using EXRGames.Application.Exceptions;
using EXRGames.Application.Requests.Tags;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXRGames.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController(IMediator mediator) : ControllerBase {
        [HttpPost("create")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Create([FromBody] CreateTagCommand request, CancellationToken token) {
            try {
                await mediator.Send(request, token);
            } catch (TagAlreadyContainsException) {
                return Conflict($"Tag {request.Tag.ToLower()} already exists!");
            }

            return Ok();
        }

        [HttpDelete("delete")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete([FromBody] DeleteTagCommand request, CancellationToken token) {
            await mediator.Send(request, token);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllTags(CancellationToken token) {
            var tags = await mediator.Send(new FetchTagsQuery(), token);
            return Ok(tags);
        }
    }
}
