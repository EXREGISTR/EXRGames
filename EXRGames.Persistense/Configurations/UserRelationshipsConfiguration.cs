using EXRGames.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EXRGames.Persistense.Configurations {
    internal class UserRelationshipsConfiguration : IEntityTypeConfiguration<UserRelationship> {
        public void Configure(EntityTypeBuilder<UserRelationship> builder) {
            builder.ToTable("user_relationships");

            builder.HasKey(x => new { x.SenderId, x.TargetId });

            builder
                .HasOne(x => x.SenderProifle)
                .WithMany(u => u.Friends)
                .HasForeignKey(x => x.SenderId);

            builder
                .HasOne(x => x.TargetProfile)
                .WithMany()
                .HasForeignKey(x => x.TargetId);
        }
    }
}
