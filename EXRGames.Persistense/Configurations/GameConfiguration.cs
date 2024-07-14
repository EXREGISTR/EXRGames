using EXRGames.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EXRGames.Persistense.Configurations {
    internal class GameConfiguration : IEntityTypeConfiguration<Game> {
        public void Configure(EntityTypeBuilder<Game> builder) {
            builder.ToTable("games");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(Game.MaxTitleLength)
                .IsRequired();
                
            builder.Property(x => x.Description)
                .IsRequired();

            builder.HasMany(x => x.Tags)
                .WithMany()
                .UsingEntity(j => j.ToTable("game_tags"));
        }
    }
}
