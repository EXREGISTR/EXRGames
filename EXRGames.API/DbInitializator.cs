using Microsoft.AspNetCore.Identity;

namespace EXRGames.API {
    internal class DbInitializator(
        UserManager<IdentityUser> userManager, 
        RoleManager<IdentityRole> roleManager) {        
        public async Task Initialize() {
            await EnsureCreatedRole(UserRoles.Superuser);
            await EnsureCreatedRole(UserRoles.Admin);
            await EnsureCreatedRole(UserRoles.User);

            await EnsureCreatedSuperuser("superuser", "superpassw@rd_99");
        }

        private async Task EnsureCreatedRole(string role) {
            if (await roleManager.FindByNameAsync(role) == null) {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task EnsureCreatedSuperuser(string login, string password) {
            var tempUser = await userManager.FindByNameAsync(login);
            if (tempUser != null) return;

            var superuser = new IdentityUser {
                Id = Guid.NewGuid().ToString(),
                UserName = login,
            };

            await userManager.CreateAsync(superuser, password);
            await userManager.AddToRoleAsync(superuser, UserRoles.Superuser);
        } 
    }
}
