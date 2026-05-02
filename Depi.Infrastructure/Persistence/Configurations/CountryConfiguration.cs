namespace DEPI.Infrastructure.Persistence.Configurations;

using DEPI.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> entity)
    {
        entity.ToTable("Countries");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        entity.Property(e => e.NameEn).IsRequired().HasMaxLength(100);
        entity.Property(e => e.Iso2).IsRequired().HasMaxLength(2);
        entity.Property(e => e.Iso3).IsRequired().HasMaxLength(3);
        entity.Property(e => e.PhoneCode).HasMaxLength(10);
        entity.Property(e => e.FlagUrl).HasMaxLength(500);
        entity.Property(e => e.DisplayOrder).HasDefaultValue(0);

        entity.HasIndex(e => e.Iso2).IsUnique();
        entity.HasIndex(e => e.Iso3).IsUnique();

        entity.HasData(
            Country.Create("مصر", "Egypt", "EG", "EGY", "+20", "🇪🇬", 1),
            Country.Create("المملكة العربية السعودية", "Saudi Arabia", "SA", "SAU", "+966", "🇸🇦", 2),
            Country.Create("الإمارات العربية المتحدة", "United Arab Emirates", "AE", "ARE", "+971", "🇦🇪", 3),
            Country.Create("الكويت", "Kuwait", "KW", "KWT", "+965", "🇰🇼", 4),
            Country.Create("قطر", "Qatar", "QA", "QAT", "+974", "🇶🇦", 5),
            Country.Create("البحرين", "Bahrain", "BH", "BHR", "+973", "🇧🇭", 6),
            Country.Create("الأردن", "Jordan", "JO", "JOR", "+962", "🇯🇴", 7),
            Country.Create("لبنان", "Lebanon", "LB", "LBN", "+961", "🇱🇧", 8),
            Country.Create("العراق", "Iraq", "IQ", "IRQ", "+964", "🇮🇶", 9),
            Country.Create("اليمن", "Yemen", "YE", "YEM", "+967", "🇾🇪", 10),
            Country.Create("سوريا", "Syria", "SY", "SYR", "+963", "🇸🇾", 11),
            Country.Create("فلسطين", "Palestine", "PS", "PSE", "+970", "🇵🇸", 12),
            Country.Create("الجزائر", "Algeria", "DZ", "DZA", "+213", "🇩🇿", 13),
            Country.Create("المغرب", "Morocco", "MA", "MAR", "+212", "🇲🇦", 14),
            Country.Create("تونس", "Tunisia", "TN", "TUN", "+216", "🇹🇳", 15),
            Country.Create("ليبيا", "Libya", "LY", "LBY", "+218", "🇱🇾", 16),
            Country.Create("السودان", "Sudan", "SD", "SDN", "+249", "🇸🇩", 17),
            Country.Create("عُمان", "Oman", "OM", "OMN", "+968", "🇴🇲", 18),
            Country.Create("تركيا", "Turkey", "TR", "TUR", "+90", "🇹🇷", 19),
            Country.Create("الولايات المتحدة", "United States", "US", "USA", "+1", "🇺🇸", 20)
        );
    }
}