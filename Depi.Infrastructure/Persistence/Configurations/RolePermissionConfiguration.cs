namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(rp => rp.Id);

        builder.Property(rp => rp.Permission)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(rp => new { rp.RoleId, rp.Permission })
            .IsUnique();

        builder.HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}