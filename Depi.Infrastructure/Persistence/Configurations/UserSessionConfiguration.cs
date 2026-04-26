namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Token)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(s => s.RefreshToken)
            .HasMaxLength(500);

        builder.Property(s => s.IpAddress)
            .HasMaxLength(45);

        builder.Property(s => s.UserAgent)
            .HasMaxLength(500);

        builder.Property(s => s.DeviceType)
            .HasMaxLength(50);

        builder.Property(s => s.Browser)
            .HasMaxLength(100);

        builder.Property(s => s.Os)
            .HasMaxLength(100);

        builder.Property(s => s.City)
            .HasMaxLength(100);

        builder.Property(s => s.Country)
            .HasMaxLength(100);

        builder.HasIndex(s => s.Token)
            .IsUnique();

        builder.HasOne(s => s.User)
            .WithMany(u => u.Sessions)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}