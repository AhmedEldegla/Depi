namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MediaFileEntityConfiguration : IEntityTypeConfiguration<MediaFile>
{
    public void Configure(EntityTypeBuilder<MediaFile> entity)
    {
        entity.ToTable("MediaFiles");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.FileName).IsRequired().HasMaxLength(260);
        entity.Property(e => e.OriginalName).IsRequired().HasMaxLength(260);
        entity.Property(e => e.FilePath).IsRequired().HasMaxLength(1000);
        entity.Property(e => e.FileExtension).HasMaxLength(20);
        entity.Property(e => e.MimeType).HasMaxLength(100);
        entity.Property(e => e.Description).HasMaxLength(500);

        entity.HasIndex(e => new { e.OwnerId, e.Type }).IsUnique();

        entity.HasOne(e => e.Owner)
            .WithMany()
            .HasForeignKey(e => e.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}