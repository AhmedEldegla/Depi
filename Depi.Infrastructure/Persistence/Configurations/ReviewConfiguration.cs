namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Comment)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(r => r.Response)
            .HasMaxLength(1000);

        builder.Property(r => r.Rating)
            .IsRequired();

        builder.HasIndex(r => r.Rating);

        builder.HasIndex(r => r.ReviewerId);

        builder.HasIndex(r => r.RevieweeId);

        builder.HasOne(r => r.Reviewer)
            .WithMany()
            .HasForeignKey(r => r.ReviewerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Reviewee)
            .WithMany()
            .HasForeignKey(r => r.RevieweeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Project)
            .WithMany()
            .HasForeignKey(r => r.ProjectId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(r => r.Contract)
            .WithMany()
            .HasForeignKey(r => r.ContractId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}