using EXRGames.Application.AccountRequests;
using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace EXRGames.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(
        IUserProfilesStore profilesStore,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IValidator<RegisterUserRequest> registrationValidator) : ControllerBase {

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken token) {
            var validationResult = registrationValidator.Validate(request);
            if (!validationResult.IsValid) {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }

            var tempUser = await userManager.FindByNameAsync(request.Username);
            if (tempUser != null) {
                return Conflict($"User with login {request.Username} already exists!");
            }

            var id = Guid.NewGuid().ToString();

            var user = new IdentityUser {
                Id = id,
                UserName = request.Username,
            };

            var profile = new UserProfile {
                Id = id,
                Nickname = request.Nickname ?? request.Username,
                RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow),
                BirthDate = DateOnly.FromDateTime(request.BirthDate),
            };

            await profilesStore.Create(profile, token);

            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            await signInManager.SignInAsync(user, isPersistent: false);
            return Ok(user.Id);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) {
            var result = await signInManager.PasswordSignInAsync(
                request.Username, request.Password, request.RememberMe, false);

            if (!result.Succeeded) {
                return BadRequest("Incorrect login and/or password");
            }

            var user = await userManager.FindByNameAsync(request.Username);
            return Ok(user!.Id);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
            return Ok();
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserRequest request, CancellationToken token) {
            var user = await userManager.FindByIdAsync(request.Id);

            if (user == null) {
                return Conflict("User doesn't exists");
            }

            var result = await userManager.CheckPasswordAsync(user, request.Password);
            if (!result) {
                return Conflict("Invalid password");
            }

            using var transactionScope = new TransactionScope();
            await userManager.DeleteAsync(user);

            var profile = await profilesStore.Fetch(request.Id, token);

            if (profile == null) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Profile doesn't exists. How its happens??? lulw...");
            }

            await profilesStore.Delete(profile!, token);

            transactionScope.Complete();
            await signInManager.SignOutAsync();
            return Ok();
        }
    }
}
