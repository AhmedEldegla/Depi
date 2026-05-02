namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserProfileEntityConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> entity)
    {
        entity.ToTable("UserProfiles");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.DisplayName).IsRequired().HasMaxLength(100);
        entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
        entity.Property(e => e.Bio).HasMaxLength(1000);
        entity.Property(e => e.HourlyRate).HasPrecision(18, 2);
        entity.Property(e => e.Timezone).HasMaxLength(100);
        entity.Property(e => e.ResponseTime).HasDefaultValue(24);
        entity.Property(e => e.CompletedProjects).HasDefaultValue(0);
        entity.Property(e => e.IsAvailable).HasDefaultValue(true);
        entity.Property(e => e.LinkedInUrl).HasMaxLength(500);
        entity.Property(e => e.PortfolioUrl).HasMaxLength(500);
        entity.Property(e => e.GithubUrl).HasMaxLength(500);
        entity.Property(e => e.WebsiteUrl).HasMaxLength(500);
        entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        entity.Property(e => e.Address).HasMaxLength(500);

        entity.HasIndex(e => e.UserId).IsUnique();

        entity.HasOne(e => e.User)
            .WithOne()
            .HasForeignKey<UserProfile>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Country)
            .WithMany()
            .HasForeignKey(e => e.CountryId)
            .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(e => e.Currency)
            .WithMany()
            .HasForeignKey(e => e.CurrencyId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}