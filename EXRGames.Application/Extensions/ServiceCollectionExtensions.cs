using EXRGames.Application.AccountRequests;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EXRGames.Application.Extensions {
    public static class ServiceCollectionExtensions {
        public static void AddMediatorAndValidators(this IServiceCollection services) {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(options => 
                options.RegisterServicesFromAssembly(assembly)
            );

            RegisterExplicitValidators(services);
        }

        private static void RegisterExplicitValidators(IServiceCollection services) {
            services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserValidator>();
        }
    }
}
