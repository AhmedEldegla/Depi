namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(p => p.BudgetMin)
            .HasPrecision(18, 2);

        builder.Property(p => p.BudgetMax)
            .HasPrecision(18, 2);

        builder.Property(p => p.FixedPrice)
            .HasPrecision(18, 2);

        builder.Property(p => p.FinalPrice)
            .HasPrecision(18, 2);

        builder.Property(p => p.Skills)
            .HasMaxLength(500);

        builder.HasIndex(p => p.Status);

        builder.HasIndex(p => p.OwnerId);

        builder.HasOne(p => p.Owner)
            .WithMany()
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(p => p.AssignedFreelancer)
            .WithMany()
            .HasForeignKey(p => p.AssignedFreelancerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(p => p.Proposals)
            .WithOne(pr => pr.Project)
            .HasForeignKey(pr => pr.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}