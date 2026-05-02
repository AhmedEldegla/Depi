namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.Property(c => c.Icon)
            .HasMaxLength(200);

        builder.HasIndex(c => c.Name)
            .IsUnique();

        builder.Navigation(c => c.Projects).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasOne(c => c.ParentCategory)
            .WithMany(pc => pc.SubCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}