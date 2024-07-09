using EXRGames.Application.Exceptions.Account;
using EXRGames.Application.Requests.Accounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXRGames.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(IMediator mediator) : ControllerBase {
        private readonly IMediator mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand request, CancellationToken token) {
            try {
                var userId = await mediator.Send(request, token);
                return Ok(userId);
            } catch (UserAlreadyRegisteredException) {
                return Conflict($"User with login {request.Username} already exists!");
            } catch (RegistrationFailedException exception) {
                return BadRequest(exception.Errors.Select(x => x.Description));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request, CancellationToken token) {
            try {
                var userId = await mediator.Send(request, token);
                return Ok(userId);
            } catch (LoginFailedException) {
                return BadRequest("Incorrect login and/or password");
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken token) {
            await mediator.Send(new LogoutCommand(), token);
            return Ok();
        }
    }
}
