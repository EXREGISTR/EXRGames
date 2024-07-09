using EXRGames.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EXRGames.Persistense.Configurations {
    internal class FriendsConfiguration : IEntityTypeConfiguration<Friend> {
        public void Configure(EntityTypeBuilder<Friend> builder) {
            builder.ToTable("friends");

            builder.HasKey(x => new { x.SenderId, x.TargetId });

            builder
                .Property(x => x.Status)
                .IsRequired();

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
