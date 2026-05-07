namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Wallets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(t => t.Fee)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(t => t.NetAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(t => t.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.RelatedEntityType)
            .HasMaxLength(100);

        builder.Property(t => t.PaymentMethod)
            .HasMaxLength(50);

        builder.Property(t => t.ExternalTransactionId)
            .HasMaxLength(100);

        builder.HasIndex(t => t.Status);

        builder.HasIndex(t => t.Type);

        builder.HasIndex(t => t.WalletId);

        builder.HasOne(t => t.Wallet)
            .WithMany(w => w.Transactions)
            .HasForeignKey(t => t.WalletId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.FromWallet)
            .WithMany()
            .HasForeignKey(t => t.FromWalletId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.ToWallet)
            .WithMany()
            .HasForeignKey(t => t.ToWalletId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}