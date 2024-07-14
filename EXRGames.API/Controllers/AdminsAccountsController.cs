using EXRGames.API.Requests.Accounts.Admin;
using EXRGames.Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EXRGames.API.Controllers {
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.Superuser)]
    [ApiController]
    public class AdminsAccountsController(
        IUnitOfWork unitOfWork,
        UserManager<IdentityUser> userManager)
        : ControllerBase {

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAdminRequest request) {
            if (await userManager.FindByNameAsync(request.Username) != null) {
                return Conflict("Admin with same login already exists!");
            }

            var id = Guid.NewGuid().ToString();

            var user = new IdentityUser {
                Id = id,
                UserName = request.Username,
            };

            using var transaction = unitOfWork.BeginTransaction();

            try {
                await userManager.CreateAsync(user, request.Password);
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
                transaction.Commit();
            } catch (Exception) {
                transaction.Rollback();
            }

            return Ok(id);
        }

        [HttpGet("all")]
        public async Task<IActionResult> FetchAllAdmins() {
            var admins = await userManager.GetUsersInRoleAsync(UserRoles.Admin);
            return Ok(admins.Select(x => x.UserName));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteAdminRequest request) {
            var user = await userManager.FindByNameAsync(request.Username);
            if (user == null) return Conflict("Unknown username");

            await userManager.DeleteAsync(user);
            return Ok();
        }
    }
}