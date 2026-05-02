namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MilestoneConfiguration : IEntityTypeConfiguration<Milestone>
{
    public void Configure(EntityTypeBuilder<Milestone> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Description)
            .HasMaxLength(1000);

        builder.Property(m => m.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasIndex(m => m.Status);

        builder.HasOne(m => m.Contract)
            .WithMany(c => c.Milestones)
            .HasForeignKey(m => m.ContractId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}