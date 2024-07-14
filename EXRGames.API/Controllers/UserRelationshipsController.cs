using EXRGames.API.Requests.UserRelationships;
using EXRGames.Application.Requests.UserRelationships;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EXRGames.API.Controllers {
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.User)]
    [ApiController]
    public class UserRelationshipsController(
        UserManager<IdentityUser> userManager,
        IMediator mediator) : ControllerBase {

        [HttpPost("send_relationship_request")]
        public async Task<IActionResult> SendFriendshipRequest([FromBody] SendRelationshipRequest request, CancellationToken token) {
            var command = new SendRelationshipCommand {
                SenderId = GetUserId(),
                AcceptorId = request.AcceptorId,
            };

            await mediator.Send(command, token);
            return Ok();
        }

        [HttpPost("update_relationship_status")]
        public async Task<IActionResult> UpdateRelationshipStatus([FromBody] UpdateRelationshipStatusRequest request, CancellationToken token) {
            var command = new UpdateRelationshipStatusCommand {
                SenderId = GetUserId(),
                AcceptorId = request.AcceptorId,
                Status = request.Status,
            };

            await mediator.Send(command, token);
            return Ok();
        }

        [HttpDelete("delete_relationship")]
        public async Task<IActionResult> DeleteRelationship([FromBody] DeleteRelationshipRequest request, CancellationToken token) {
            var command = new DeleteRelationshipCommand {
                SenderId = GetUserId(),
                AcceptorId = request.AcceptorId,
            };

            await mediator.Send(command, token);
            return Ok();
        }

        [HttpGet("incoming_requests")]
        public async Task<IActionResult> FetchIncomingRequests(CancellationToken token) {
            var query = new FetchIncomingRequestsQuery {
                UserId = GetUserId(),
            };

            var users = await mediator.Send(query, token);
            return Ok(users);
        }

        [HttpGet("outgoing_requests")]
        public async Task<IActionResult> FetchOutgoingRequests(CancellationToken token) {
            var query = new FetchOutgoingRequestsQuery {
                UserId = GetUserId(),
            };

            var users = await mediator.Send(query, token);
            return Ok(users);
        }

        [HttpGet("friends")]
        public async Task<IActionResult> FetchFriends([FromQuery] FetchFriendsRequest request, CancellationToken token) {
            var query = new FetchFriendsQuery { 
                UserId = GetUserId(), 
                Status = request.Status 
            };

            var users = await mediator.Send(query, token);
            return Ok(users);
        }

        private string GetUserId() => userManager.GetUserId(HttpContext.User)!;
    }
}
