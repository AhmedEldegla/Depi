namespace DEPI.Domain.Entities.Shared;

using DEPI.Domain.Common.Base;

public class Currency : AuditableEntity
{
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Symbol { get; private set; } = string.Empty;
    public decimal ExchangeRate { get; private set; } = 1m;
    public bool IsActive { get; private set; } = true;
    public bool IsDefault { get; private set; }

    private Currency() { }

    public static Currency Create(string code, string name, string symbol, bool isDefault = false)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("كود العملة مطلوب", nameof(code));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("اسم العملة مطلوب", nameof(name));

        return new Currency
        {
            Code = code.ToUpperInvariant().Trim(),
            Name = name.Trim(),
            Symbol = symbol?.Trim() ?? code,
            ExchangeRate = 1m,
            IsDefault = isDefault,
            IsActive = true
        };
    }

    public void UpdateExchangeRate(decimal rate)
    {
        if (rate <= 0)
            throw new ArgumentException("سعر الصرف يجب أن يكون أكبر من صفر");

        ExchangeRate = rate;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}