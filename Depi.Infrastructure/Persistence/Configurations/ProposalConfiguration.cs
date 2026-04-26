namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Proposals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
{
    public void Configure(EntityTypeBuilder<Proposal> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ProposedAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.CoverLetter)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(p => p.RejectionReason)
            .HasMaxLength(500);

        builder.HasIndex(p => p.Status);

        builder.HasIndex(p => p.ProjectId);

        builder.HasIndex(p => p.FreelancerId);

        builder.HasOne(p => p.Project)
            .WithMany(pr => pr.Proposals)
            .HasForeignKey(p => p.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Freelancer)
            .WithMany()
            .HasForeignKey(p => p.FreelancerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}