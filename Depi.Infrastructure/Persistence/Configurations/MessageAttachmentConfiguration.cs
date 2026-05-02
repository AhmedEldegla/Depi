namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MessageAttachmentConfiguration : IEntityTypeConfiguration<MessageAttachment>
{
    public void Configure(EntityTypeBuilder<MessageAttachment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.FileName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.FileUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(a => a.FileType)
            .HasMaxLength(100);

        builder.HasIndex(a => a.MessageId);

        builder.HasOne(a => a.Message)
            .WithMany(m => m.Attachments)
            .HasForeignKey(a => a.MessageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}