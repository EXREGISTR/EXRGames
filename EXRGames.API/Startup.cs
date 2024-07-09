using EXRGames.API.Extensions;
using EXRGames.Persistense;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EXRGames.API {
    public class Startup {
        public void ConfigureServices(IServiceCollection services) {
            services.AddProjectDependencies();

            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;

                options.Lockout.MaxFailedAccessAttempts = 5;
            }).AddEntityFrameworkStores<ApplicationContext>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => {
                    options.Events.OnRedirectToLogin = context => {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                });
            services.AddAuthorization();
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment enviroment, IServiceProvider services) {
            if (enviroment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            await InitializeDatabase(services);
        }

        private static async Task InitializeDatabase(IServiceProvider services) {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            context.Database.Migrate();

            var initializator = scope.ServiceProvider.GetRequiredService<DbInitializator>();
            await initializator.Initialize();
        } 
    }
}
