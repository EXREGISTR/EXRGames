using EXRGames.Domain.Interfaces;
using EXRGames.Persistense.Repositories;

namespace EXRGames.API.Extensions {
    internal static class ServiceCollectionExtensions {
        public static void AddProjectDependencies(this IServiceCollection services) {
            services.AddScoped<IGamesStore, GamesStore>();
            services.AddScoped<ITagsStore, TagsStore>();
            services.AddScoped<IUserProfilesStore, UserProfilesStore>();
        }
    }
}
