using EXRGames.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EXRGames.Persistense.Configurations {
    internal class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile> {
        public void Configure(EntityTypeBuilder<UserProfile> builder) {
            builder.ToTable("user_profiles");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nickname)
                .IsRequired()
                .HasMaxLength(UserProfile.MaxNicknameLength);
        }
    }

    internal class TagConfiguration : IEntityTypeConfiguration<Tag> {
        public void Configure(EntityTypeBuilder<Tag> builder) {
            builder.ToTable("tags");

            builder.HasKey(x => x.Name);
        }
    }
}
