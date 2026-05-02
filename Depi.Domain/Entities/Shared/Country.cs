namespace DEPI.Domain.Entities.Shared;

using DEPI.Domain.Common.Base;

public class Country : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string NameEn { get; private set; } = string.Empty;
    public string Iso2 { get; private set; } = string.Empty;
    public string Iso3 { get; private set; } = string.Empty;
    public string? PhoneCode { get; private set; }
    public string? FlagUrl { get; private set; }
    public bool IsActive { get; private set; } = true;
    public int DisplayOrder { get; private set; }

    private Country() { }

    public static Country Create(string name, string nameEn, string iso2, string iso3, string? phoneCode = null, string? flagUrl = null, int displayOrder = 0)
    {
        return new Country
        {
            Name = name,
            NameEn = nameEn,
            Iso2 = iso2.ToUpperInvariant(),
            Iso3 = iso3.ToUpperInvariant(),
            PhoneCode = phoneCode,
            FlagUrl = flagUrl,
            IsActive = true,
            DisplayOrder = displayOrder
        };
    }

    public void Activate()
    {
        if (IsActive)
            throw new InvalidOperationException("Country is already active");
        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("Country is already inactive");
        IsActive = false;
    }
}