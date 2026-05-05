namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Verifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class IdentityVerificationConfiguration : IEntityTypeConfiguration<IdentityVerification>
{
    public void Configure(EntityTypeBuilder<IdentityVerification> builder)
    {
        builder.ToTable("IdentityVerifications");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.DocumentType)
            .IsRequired();

        builder.Property(v => v.DocumentImageUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(v => v.SelfieImageUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(v => v.Status)
            .IsRequired();

        builder.Property(v => v.RejectionReason)
            .HasMaxLength(1000);

        builder.HasOne(v => v.User)
            .WithMany()
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(v => new { v.UserId, v.Status });
    }
}
