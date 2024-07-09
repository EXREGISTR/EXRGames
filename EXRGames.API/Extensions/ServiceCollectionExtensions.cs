using EXRGames.Application.Extensions;
using EXRGames.Persistense.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace EXRGames.API.Extensions {
    internal static class ServiceCollectionExtensions {
        public static void AddProjectDependencies(this IServiceCollection services) {
            services.AddTransient<DbInitializator>();
            services.AddStores();
            services.AddMediatorAndValidators();
            AddFluentValidation(services);
        }

        private static void AddFluentValidation(IServiceCollection services) {
            var assembly = typeof(Application.Extensions.ServiceCollectionExtensions).Assembly;
            services
                .AddValidatorsFromAssembly(assembly);

            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
