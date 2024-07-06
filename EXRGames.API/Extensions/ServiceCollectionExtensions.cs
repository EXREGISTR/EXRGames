using EXRGames.Application.Extensions;
using EXRGames.Persistense.Extensions;

namespace EXRGames.API.Extensions {
    internal static class ServiceCollectionExtensions {
        public static void AddProjectDependencies(this IServiceCollection services) {
            services.AddStores();
            services.AddMediatorAndValidators();
        }
    }
}
