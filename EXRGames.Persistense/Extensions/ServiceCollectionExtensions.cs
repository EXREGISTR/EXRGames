using EXRGames.Domain.Interfaces;
using EXRGames.Persistense.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EXRGames.Persistense.Extensions {
    public static class ServiceCollectionExtensions {
        public static void AddStores(this IServiceCollection services) {
            services.AddScoped<IGamesStore, GamesStore>();
            services.AddScoped<ITagsStore, TagsStore>();
            services.AddScoped<IUserProfilesStore, UserProfilesStore>();
        }
    }
}
