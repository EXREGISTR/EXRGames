using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EXRGames.Application.Extensions {
    public static class ServiceCollectionExtensions {
        public static void AddMediatorAndValidators(this IServiceCollection services) {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(options => 
                options.RegisterServicesFromAssembly(assembly)
            );

            services.AddValidatorsFromAssembly(assembly);
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
