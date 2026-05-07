namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FreelancerSkillEntityConfiguration : IEntityTypeConfiguration<FreelancerSkill>
{
    public void Configure(EntityTypeBuilder<FreelancerSkill> entity)
    {
        entity.ToTable("FreelancerSkills");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.ProficiencyLevel).IsRequired();
        entity.Property(e => e.YearsOfExperience).IsRequired();
        entity.Property(e => e.IsVerified).HasDefaultValue(false);
        entity.Property(e => e.VerificationProof).HasMaxLength(1000);

        entity.HasIndex(e => new { e.UserId, e.SkillId }).IsUnique();

        entity.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Skill)
            .WithMany()
            .HasForeignKey(e => e.SkillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class PortfolioItemEntityConfiguration : IEntityTypeConfiguration<PortfolioItem>
{
    public void Configure(EntityTypeBuilder<PortfolioItem> entity)
    {
        entity.ToTable("PortfolioItems");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
        entity.Property(e => e.Description).IsRequired().HasMaxLength(2000);
        entity.Property(e => e.Url).HasMaxLength(1000);
        entity.Property(e => e.LiveUrl).HasMaxLength(1000);
        entity.Property(e => e.IsFeatured).HasDefaultValue(false);
        entity.Property(e => e.IsPublished).HasDefaultValue(false);
        entity.Property(e => e.ViewCount).HasDefaultValue(0);
        entity.Property(e => e.DisplayOrder).HasDefaultValue(0);

        entity.HasIndex(e => new { e.UserId, e.IsPublished });

        entity.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ServicePackageEntityConfiguration : IEntityTypeConfiguration<ServicePackage>
{
    public void Configure(EntityTypeBuilder<ServicePackage> entity)
    {
        entity.ToTable("ServicePackages");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
        entity.Property(e => e.Price).HasPrecision(18, 2).HasDefaultValue(0);
        entity.Property(e => e.DeliveryDays).IsRequired();
        entity.Property(e => e.Revisions).HasDefaultValue(0);
        entity.Property(e => e.IsActive).HasDefaultValue(true);
        entity.Property(e => e.IsFeatured).HasDefaultValue(false);

        entity.HasIndex(e => new { e.UserId, e.IsActive });

        entity.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Currency)
            .WithMany()
            .HasForeignKey(e => e.CurrencyId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}