using EXRGames.API.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXRGames.API.Controllers {
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.Superuser)]
    [ApiController]
    public class AdminAccountsController : ControllerBase {

    }
}
