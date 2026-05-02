namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(c => c.Symbol)
            .HasMaxLength(8);

        builder.Property(c => c.ExchangeRate)
            .HasPrecision(18, 6);

        builder.HasIndex(c => c.Code).IsUnique();
        builder.HasIndex(c => c.Name);

        builder.HasData(GetSeedCurrencies());
    }

    private static IEnumerable<Currency> GetSeedCurrencies()
    {
        var currencies = new List<Currency>
        {
            Currency.Create("USD", "US Dollar", "$", true),
            Currency.Create("EGP", "Egyptian Pound", "E£"),
            Currency.Create("GBP", "British Pound", "£"),
            Currency.Create("EUR", "Euro", "€"),
            Currency.Create("SAR", "Saudi Riyal", "﷼"),
            Currency.Create("AED", "UAE Dirham", "د.إ"),
            Currency.Create("KWD", "Kuwaiti Dinar", "د.ك"),
            Currency.Create("QAR", "Qatari Riyal", "﷼"),
            Currency.Create("BHD", "Bahraini Dinar", ".د.ب"),
            Currency.Create("OMR", "Omani Rial", "﷼"),
            Currency.Create("JOD", "Jordanian Dinar", "د.أ"),
            Currency.Create("CAD", "Canadian Dollar", "C$"),
            Currency.Create("AUD", "Australian Dollar", "A$"),
            Currency.Create("INR", "Indian Rupee", "₹"),
        };

        return currencies;
    }
}