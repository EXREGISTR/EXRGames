using EXRGames.API.Requests.Accounts.User;
using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using EXRGames.Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EXRGames.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(
        IUserProfilesStore profilesStore,
        IUnitOfWork unitOfWork,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager) : ControllerBase {

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken token) {
            var tempUser = await userManager.FindByNameAsync(request.Username);
            if (tempUser != null) {
                return Conflict($"User with login {request.Username} already exists!");
            }

            var id = Guid.NewGuid().ToString();

            var result = await RegisterInternal(id, request, token);

            if (!result) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(id);
        }

        private async Task<bool> RegisterInternal(string userId, RegisterUserRequest request, CancellationToken token) {
            var user = new IdentityUser {
                Id = userId,
                UserName = request.Username,
            };

            var profile = new UserProfile {
                Id = userId,
                Nickname = request.Nickname ?? request.Username,
                RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow),
                BirthDate = request.BirthDate != null
                    ? DateOnly.FromDateTime(request.BirthDate.Value)
                    : null,
            };

            using var transaction = unitOfWork.BeginTransaction();

            try {
                var result = await userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded) {
                    return false;
                }

                await userManager.AddToRoleAsync(user, UserRoles.User);

                await profilesStore.Create(profile, token);

                await signInManager.SignInAsync(user, isPersistent: false);

                transaction.Commit();
            } catch (Exception) {
                transaction.Rollback();
                return false;
            }

            return true;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) {
            var result = await signInManager.PasswordSignInAsync(
                request.Username, request.Password, request.RememberMe, false);

            if (!result.Succeeded) {
                return BadRequest("Incorrect login or/and password");
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

            using var transaction = unitOfWork.BeginTransaction();

            try {
                await DeleteInternal(user, token);
                transaction.Commit();
            } catch (Exception) {
                transaction.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        private async Task DeleteInternal(IdentityUser user, CancellationToken token) {
            await userManager.DeleteAsync(user);

            var profile = await profilesStore.Fetch(user.Id, token);

            if (profile != null) {
                await profilesStore.Delete(profile, token);
            }

            await signInManager.SignOutAsync();
        }
    }
}
