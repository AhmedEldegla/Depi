namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Wallets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.Balance)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(w => w.PendingBalance)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(w => w.TotalEarnings)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(w => w.TotalSpent)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(w => w.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.HasIndex(w => w.UserId)
            .IsUnique();

        builder.HasOne(w => w.User)
            .WithOne()
            .HasForeignKey<Wallet>(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.Transactions)
            .WithOne(t => t.Wallet)
            .HasForeignKey(t => t.WalletId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}