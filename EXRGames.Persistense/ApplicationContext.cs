using EXRGames.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace EXRGames.Persistense {
    public class ApplicationContext : IdentityDbContext {
        private readonly IConfiguration configuration;

        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, 
            IConfiguration configuration) : base(options) {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
            var connectionString = configuration.GetConnectionString("Connection");
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)));
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
