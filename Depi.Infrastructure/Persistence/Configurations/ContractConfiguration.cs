namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.TotalAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(c => c.CancellationReason)
            .HasMaxLength(500);

        builder.Property(c => c.DisputeReason)
            .HasMaxLength(500);

        builder.HasIndex(c => c.Status);

        builder.HasIndex(c => c.ProjectId)
            .IsUnique();

        builder.HasOne(c => c.Project)
            .WithOne()
            .HasForeignKey<Contract>(c => c.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Client)
            .WithMany()
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Freelancer)
            .WithMany()
            .HasForeignKey(c => c.FreelancerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Milestones)
            .WithOne(m => m.Contract)
            .HasForeignKey(m => m.ContractId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}