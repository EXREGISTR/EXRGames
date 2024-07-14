using EXRGames.Domain;
using EXRGames.Domain.Contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;

namespace EXRGames.Persistense {
    public class ApplicationContext : IdentityDbContext, IUnitOfWork {
        private readonly IConfiguration configuration;

        internal DbSet<Game> Games { get; set; }
        internal DbSet<Tag> Tags { get; set; }
        internal DbSet<UserProfile> UserProfiles { get; set; }
        internal DbSet<UserRelationship> Friends { get; set; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public ApplicationContext() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

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

        public IDbTransaction BeginTransaction(IsolationLevel level) => Database.BeginTransaction(level).GetDbTransaction();
    }
}
