namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Wallets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EscrowConfiguration : IEntityTypeConfiguration<Escrow>
{
    public void Configure(EntityTypeBuilder<Escrow> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(e => e.ReleaseAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(e => e.Fee)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(e => e.Description)
            .HasMaxLength(500);

        builder.HasIndex(e => e.Status);

        builder.HasOne(e => e.Contract)
            .WithMany()
            .HasForeignKey(e => e.ContractId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.ClientWallet)
            .WithMany()
            .HasForeignKey(e => e.ClientWalletId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FreelancerWallet)
            .WithMany()
            .HasForeignKey(e => e.FreelancerWalletId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}