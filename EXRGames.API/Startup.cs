using EXRGames.API.Extensions;
using EXRGames.Application.Requests;
using EXRGames.Persistense;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace EXRGames.API {
    public class Startup {
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<ApplicationContext>(optionsLifetime: ServiceLifetime.Singleton);

            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.Lockout.MaxFailedAccessAttempts = 5;
            }).AddEntityFrameworkStores<ApplicationContext>();

            services.AddProjectDependencies();

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

            services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(PaginableQuery).Assembly));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment enviroment) {
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
        }
    }
}
