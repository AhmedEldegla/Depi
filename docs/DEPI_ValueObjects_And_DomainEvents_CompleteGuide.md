# Deep Dive: Value Objects & Domain Events
## The Complete Guide with Best Practices

---

## Table of Contents

1. [Value Objects - الأساس](#1-value-objects---الأساس)
2. [Value Objects - الكود الصحيح](#2-value-objects---الكود-الصحيح)
3. [Value Objects - Best Practices](#3-value-objects---best-practices)
4. [Domain Events - الأساس](#4-domain-events---الأساس)
5. [Domain Events - الكود الصحيح](#5-domain-events---الكود-الصحيح)
6. [Domain Events - Best Practices](#6-domain-events---best-practices)
7. [Integration - Value Objects + Events مع بعض](#7-integration---value-objects--events-مع-بعض)
8. [Real Project Examples](#8-real-project-examples)

---

# PART 1: VALUE OBJECTS

## 1. Value Objects - الأساس

### 1.1 إيه هو Value Object؟

```
🎯 التعريف:
   Value Object = object بسيط ليه identity بناءً على values بتاعته
   مش بناءً على reference/ID

📦 أمثلة من الحقيقي:
   ├── Money ($100) = ليه identity بناءً على amount + currency
   ├── Email (ahmed@example.com) = ليه identity بناءً على value
   ├── Address (123 Main St) = ليه identity بناءً على كل الـ values
   └── Phone (+20 123 456 789) = ليه identity بناءً على value

❌ مثال مش Value Object:
   ├── User = ليه identity بناءً على ID مش values
   ├── Project = ليه identity بناءً على ID مش values
   └── Order = ليه identity بناءً على ID مش values
```

### 1.2 لية بنستخدم Value Objects؟

```
❌ بدون Value Object:

public class Wallet
{
    public decimal Balance { get; set; }           // رقم بس
    public string CurrencyCode { get; set; }      // string بس
}

// Problems:
wallet1.Balance = 100;
wallet1.CurrencyCode = "USD";

wallet2.Balance = 50;
wallet2.CurrencyCode = "EGP";

// ❌ Compilation = 150$؟ ده غلط!
// ❌ مفيش type safety
// ❌ مفيش validation

✅ مع Value Object:

public class Money
{
    public decimal Amount { get; }
    public Currency Currency { get; }
}

// Benefits:
var wallet1Money = new Money(100, Currency.USD);
var wallet2Money = new Money(50, Currency.EGP);

// ✅ سهل نطابق
// ✅ Type Safety
// ✅ Validation داخلي
// ✅ compiler ي caught الأخطاء
```

### 1.3 لية بنعمل Value Object immutable؟

```
❌ Mutable (متغير):

var money = new Money(100, Currency.USD);
money.Amount = 200;  // ✅ ممكن نغير

var wallet1 = new Money(100, Currency.USD);
var wallet2 = wallet1;  // same reference

wallet2.Amount = 200;

console.log(wallet1.Amount);  // 200 ❌ غلط!

✅ Immutable (مش بيتغير):

var money = new Money(100, Currency.USD);
// money.Amount = 200; ❌ مش هينفع!

// لو عايز نغير:
var newMoney = new Money(200, Currency.USD);

var wallet1 = new Money(100, Currency.USD);
var wallet2 = new Money(wallet1.Amount + 50, wallet1.Currency);
// wallet2 = new Money(150, Currency.USD)

console.log(wallet1.Amount);  // 100 ✅ صح!
```

### 1.4 متى بنستخدم Value Object؟

```
✅ بنستخدم Value Object لو:

1️⃣ Object ليه values مش identity:
   ├── Email = "ahmed@example.com" (مش ليه ID)
   ├── Money = 100$ (مش ليه ID)
   └── Address = "123 Main St" (مش ليه ID)

2️⃣ Values بتاعته بيتكرروا:
   ├── Price في products كتير
   ├── Email في users كتير
   └── Address في orders كتير

3️⃣ لازم نطابق based على values:
   ├── Email1 == Email2 (نقارن بالـ value)
   └── Address1 == Address2 (نقارن بكل الـ values)

4️⃣ Values ليهم validation logic:
   ├── Email = لازم يكون valid format
   ├── Phone = لازم يكون أرقام بس
   └── Money = لازم يكون positive

❌ مبنستخدمش Value Object لو:

1️⃣ Object ليه identity مستقل:
   ├── User (لييه ID)
   ├── Project (لييه ID)
   └── Order (لييه ID)

2️⃣ Object بيتغير:
   ├── Order.Status (بيتغير من pending ل completed)
   └── User.IsActive (بيتغير)

3️⃣ لازم نرجعه أو نمسحه:
   ├── User (نمسحه)
   └── Project (نرفضه)
```

---

## 2. Value Objects - الكود الصحيح

### 2.1 Base Value Object

```csharp
// DEPI.Domain/Common/ValueObjects/ValueObject.cs

namespace DEPI.Domain.Common.ValueObjects;

public abstract class ValueObject
{
    /// <summary>
    /// بترجع الـ components اللي بنقارن بيهم
    /// مثال: Email = ["ahmed@example.com"]
    /// مثال: Address = ["123", "Main St", "Cairo", "Egypt"]
    /// </summary>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    /// نقارن Value Objects ببعضهم
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj.GetType() != GetType())
            return false;

        var valueObject = (ValueObject)obj;

        return GetEqualityComponents()
            .SequenceEqual(valueObject.GetEqualityComponents());
    }

    /// <summary>
    ///_operator overload for ==
    /// </summary>
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    /// <summary>
    ///_operator overload for !=
    /// </summary>
    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// لازم ن override GetHashCode عشان يعمل صح مع Equals
    /// </summary>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
}
```

### 2.2 Email Value Object

```csharp
// DEPI.Domain/Modules/Identity/ValueObjects/Email.cs

namespace DEPI.Domain.Modules.Identity.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Factory method - لازم نستخدم ده بدل الـ constructor
    /// </summary>
    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email), "Email cannot be empty");

        email = email.Trim().ToLowerInvariant();

        if (!IsValidEmailFormat(email))
            throw new ArgumentException($"Invalid email format: {email}", nameof(email));

        if (email.Length > 256)
            throw new ArgumentException("Email cannot exceed 256 characters", nameof(email));

        return new Email(email);
    }

    /// <summary>
    /// Factory method - من غير exception
    /// </summary>
    public static TryResult<Email> TryCreate(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return TryResult<Email>.Failure("Email cannot be empty");

        email = email.Trim().ToLowerInvariant();

        if (!IsValidEmailFormat(email))
            return TryResult<Email>.Failure($"Invalid email format: {email}");

        if (email.Length > 256)
            return TryResult<Email>.Failure("Email cannot exceed 256 characters");

        return TryResult<Email>.Success(new Email(email));
    }

    private static bool IsValidEmailFormat(string email)
    {
        try
        {
            var regex = new System.Text.RegularExpressions.Regex(
                @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
                System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// لازم ن implement الـ protected method
    /// </summary>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }

    /// <summary>
    /// implicit conversion to string
    /// </summary>
    public static implicit operator string(Email email) => email.Value;

    /// <summary>
    /// لازم ن override ToString
    /// </summary>
    public override string ToString() => Value;
}
```

### 2.3 TryResult (بدون exceptions)

```csharp
// DEPI.Domain/Common/Results/TryResult.cs

namespace DEPI.Domain.Common.Results;

public class TryResult<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string? Error { get; }

    private TryResult(bool isSuccess, T? value, string? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static TryResult<T> Success(T value)
    {
        return new TryResult<T>(true, value, null);
    }

    public static TryResult<T> Failure(string error)
    {
        return new TryResult<T>(false, default, error);
    }

    /// <summary>
    /// map the result to another type
    /// </summary>
    public TryResult<K> Map<K>(Func<T, K> map)
    {
        if (IsFailure)
            return TryResult<K>.Failure(Error!);

        return TryResult<K>.Success(map(Value!));
    }
}
```

### 2.4 Money Value Object

```csharp
// DEPI.Domain/Modules/Payments/ValueObjects/Money.cs

namespace DEPI.Domain.Modules.Payments.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; }
    public Currency Currency { get; }

    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    /// <summary>
    /// Factory method
    /// </summary>
    public static Money Create(decimal amount, Currency currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative", nameof(amount));

        // تقريب لـ 2 decimal places
        amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);

        return new Money(amount, currency);
    }

    /// <summary>
    /// Factory method - من غير exception
    /// </summary>
    public static TryResult<Money> TryCreate(decimal amount, Currency currency)
    {
        if (amount < 0)
            return TryResult<Money>.Failure("Amount cannot be negative");

        amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);

        return TryResult<Money>.Success(new Money(amount, currency));
    }

    /// <summary>
    /// Factory method - من string (parsing)
    /// </summary>
    public static TryResult<Money> Parse(string amount, string currencyCode)
    {
        if (!decimal.TryParse(amount, out var parsedAmount))
            return TryResult<Money>.Failure($"Invalid amount: {amount}");

        if (!Enum.TryParse<Currency>(currencyCode, true, out var currency))
            return TryResult<Money>.Failure($"Invalid currency: {currencyCode}");

        return TryCreate(parsedAmount, currency);
    }

    #region Operations

    /// <summary>
    /// جمع فلوس نفس العملة
    /// </summary>
    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    /// <summary>
    /// طرح فلوس نفس العملة
    /// </summary>
    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);
        
        var result = Amount - other.Amount;
        if (result < 0)
            throw new InvalidOperationException("Cannot subtract more than available");

        return new Money(result, Currency);
    }

    /// <summary>
    /// ضرب في عدد (ل حساب discount مثلاً)
    /// </summary>
    public Money Multiply(decimal multiplier)
    {
        if (multiplier < 0)
            throw new ArgumentException("Multiplier cannot be negative", nameof(multiplier));

        return new Money(Amount * multiplier, Currency);
    }

    /// <summary>
    /// تحويل لنسبة مئوية
    /// </summary>
    public decimal ToPercentage()
    {
        return Amount * 100;
    }

    /// <summary>
    /// تحويل من percentage
    /// </summary>
    public static Money FromPercentage(decimal percentage, Currency currency)
    {
        return Create(percentage / 100, currency);
    }

    /// <summary>
    /// تحويل لعملة تانية (محتاج exchange rate)
    /// </summary>
    public Money ConvertTo(Currency targetCurrency, decimal exchangeRate)
    {
        if (Currency == targetCurrency)
            return this;

        var convertedAmount = Amount * exchangeRate;
        return new Money(convertedAmount, targetCurrency);
    }

    #endregion

    #region Comparisons

    /// <summary>
    /// هل الفلوس موجبة
    /// </summary>
    public bool IsPositive => Amount > 0;

    /// <summary>
    /// هل الفلوس سالبة
    /// </summary>
    public bool IsNegative => Amount < 0;

    /// <summary>
    /// هل الفلوس صفر
    /// </summary>
    public bool IsZero => Amount == 0;

    public static bool operator >(Money left, Money right)
    {
        left.EnsureSameCurrency(right);
        return left.Amount > right.Amount;
    }

    public static bool operator <(Money left, Money right)
    {
        left.EnsureSameCurrency(right);
        return left.Amount < right.Amount;
    }

    public static bool operator >=(Money left, Money right)
    {
        left.EnsureSameCurrency(right);
        return left.Amount >= right.Amount;
    }

    public static bool operator <=(Money left, Money right)
    {
        left.EnsureSameCurrency(right);
        return left.Amount <= right.Amount;
    }

    #endregion

    private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException(
                $"Cannot operate on different currencies. " +
                $"Left: {Currency}, Right: {other.Currency}");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    /// <summary>
    /// Format للعرض
    /// </summary>
    public string Format()
    {
        var symbol = Currency switch
        {
            Currency.USD => "$",
            Currency.EGP => "E£",
            Currency.EUR => "€",
            Currency.GBP => "£",
            _ => Currency.ToString()
        };

        return $"{symbol}{Amount:N2}";
    }

    public override string ToString() => Format();

    public static Money Zero(Currency currency) => Create(0, currency);
}
```

### 2.5 Address Value Object

```csharp
// DEPI.Domain/Modules/References/ValueObjects/Address.cs

namespace DEPI.Domain.Modules.References.ValueObjects;

public class Address : ValueObject
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }
    public string Country { get; }

    private Address(
        string street,
        string city,
        string state,
        string postalCode,
        string country)
    {
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }

    /// <summary>
    /// Factory method
    /// </summary>
    public static Address Create(
        string street,
        string city,
        string state,
        string postalCode,
        string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street is required", nameof(street));

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City is required", nameof(city));

        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country is required", nameof(country));

        return new Address(
            street.Trim(),
            city.Trim(),
            state?.Trim() ?? string.Empty,
            postalCode?.Trim() ?? string.Empty,
            country.Trim()
        );
    }

    /// <summary>
    /// Factory method - من غير exceptions
    /// </summary>
    public static TryResult<Address> TryCreate(
        string street,
        string city,
        string state,
        string postalCode,
        string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            return TryResult<Address>.Failure("Street is required");

        if (string.IsNullOrWhiteSpace(city))
            return TryResult<Address>.Failure("City is required");

        if (string.IsNullOrWhiteSpace(country))
            return TryResult<Address>.Failure("Country is required");

        return TryResult<Address>.Success(new Address(
            street.Trim(),
            city.Trim(),
            state?.Trim() ?? string.Empty,
            postalCode?.Trim() ?? string.Empty,
            country.Trim()
        ));
    }

    /// <summary>
    /// Format كامل
    /// </summary>
    public string FormatFull()
    {
        var parts = new List<string> { Street };

        if (!string.IsNullOrEmpty(State))
            parts.Add(State);

        parts.Add(City);
        parts.Add(PostalCode);
        parts.Add(Country);

        return string.Join(", ", parts);
    }

    /// <summary>
    /// Format قصير
    /// </summary>
    public string FormatShort()
    {
        return $"{City}, {Country}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street.ToUpperInvariant();
        yield return City.ToUpperInvariant();
        yield return State.ToUpperInvariant();
        yield return PostalCode.ToUpperInvariant();
        yield return Country.ToUpperInvariant();
    }

    public override string ToString() => FormatFull();
}
```

### 2.6 Phone Number Value Object

```csharp
// DEPI.Domain/Modules/Identity/ValueObjects/PhoneNumber.cs

namespace DEPI.Domain.Modules.Identity.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string CountryCode { get; }
    public string Number { get; }
    public string FormattedNumber { get; }

    private PhoneNumber(string countryCode, string number)
    {
        CountryCode = countryCode;
        Number = number;
        FormattedNumber = Format(countryCode, number);
    }

    public static PhoneNumber Create(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required", nameof(phoneNumber));

        // تنظيف الـ phone number
        phoneNumber = phoneNumber.Trim();
        
        // إزالة أي characters غير أرقام
        var digits = new string(phoneNumber.Where(char.IsDigit).ToArray());

        if (digits.Length < 8)
            throw new ArgumentException("Phone number is too short", nameof(phoneNumber));

        if (digits.Length > 15)
            throw new ArgumentException("Phone number is too long", nameof(phoneNumber));

        // استخراج country code
        string countryCode;
        string number;

        if (phoneNumber.StartsWith("+"))
        {
            // International format: +20 123 456 789
            countryCode = "+" + digits.Substring(0, Math.Min(3, digits.Length - 8));
            number = digits.Substring(countryCode.Length - 1);
        }
        else
        {
            // Local format: 20 123 456 789
            countryCode = "+" + digits.Substring(0, Math.Min(3, digits.Length - 8));
            number = digits.Substring(countryCode.Length - 1);
        }

        return new PhoneNumber(countryCode, number);
    }

    private static string Format(string countryCode, string number)
    {
        if (number.Length <= 7)
            return $"{countryCode} {number}";

        // Format: +XX XXX XXX XXXX
        var parts = new List<string>();
        var remaining = number;

        while (remaining.Length > 0)
        {
            if (remaining.Length > 3)
            {
                parts.Add(remaining.Substring(0, 3));
                remaining = remaining.Substring(3);
            }
            else
            {
                parts.Add(remaining);
                remaining = "";
            }
        }

        return $"{countryCode} {string.Join(" ", parts)}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return Number;
    }

    public override string ToString() => FormattedNumber;
}
```

### 2.7 FullName Value Object

```csharp
// DEPI.Domain/Modules/Identity/ValueObjects/FullName.cs

namespace DEPI.Domain.Modules.Identity.ValueObjects;

public class FullName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }
    public string MiddleName { get; }
    public string FullNameFormatted { get; }

    private FullName(string firstName, string lastName, string? middleName)
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        MiddleName = middleName?.Trim() ?? string.Empty;

        // بناء الـ full name
        if (string.IsNullOrEmpty(MiddleName))
            FullNameFormatted = $"{FirstName} {LastName}";
        else
            FullNameFormatted = $"{FirstName} {MiddleName} {LastName}";
    }

    public static FullName Create(string firstName, string lastName, string? middleName = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required", nameof(lastName));

        if (firstName.Length < 2)
            throw new ArgumentException("First name is too short", nameof(firstName));

        if (lastName.Length < 2)
            throw new ArgumentException("Last name is too short", nameof(lastName));

        return new FullName(firstName, lastName, middleName);
    }

    /// <summary>
    /// من اسم كامل (Ahmed Mohamed Ali)
    /// </summary>
    public static FullName CreateFromFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required", nameof(fullName));

        var parts = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 0)
            throw new ArgumentException("Full name is required", nameof(fullName));

        if (parts.Length == 1)
            return new FullName(parts[0], "", null);

        if (parts.Length == 2)
            return new FullName(parts[0], parts[1], null);

        // parts.Length >= 3
        return new FullName(parts[0], parts[^1], string.Join(" ", parts.Skip(1).Take(parts.Length - 2)));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName.ToUpperInvariant();
        yield return LastName.ToUpperInvariant();
        yield return MiddleName.ToUpperInvariant();
    }

    public override string ToString() => FullNameFormatted;
}
```

### 2.8 Percentage Value Object

```csharp
// DEPI.Domain/Common/ValueObjects/Percentage.cs

namespace DEPI.Domain.Common.ValueObjects;

public class Percentage : ValueObject
{
    public decimal Value { get; }

    private Percentage(decimal value)
    {
        Value = value;
    }

    /// <summary>
    /// Factory method - من decimal (0.10 = 10%)
    /// </summary>
    public static Percentage Create(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Percentage cannot be negative", nameof(value));

        if (value > 1)
            throw new ArgumentException("Percentage cannot exceed 100% (1.0)", nameof(value));

        return new Percentage(Math.Round(value, 4, MidpointRounding.AwayFromZero));
    }

    /// <summary>
    /// Factory method - من string ("10" = 10%)
    /// </summary>
    public static Percentage FromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Percentage is required", nameof(value));

        // إزالة علامة %
        value = value.TrimEnd('%');

        if (!decimal.TryParse(value, out var parsed))
            throw new ArgumentException($"Invalid percentage value: {value}", nameof(value));

        return Create(parsed / 100);
    }

    /// <summary>
    /// تحويل لـ decimal (0.10)
    /// </summary>
    public decimal ToDecimal() => Value;

    /// <summary>
    /// تحويل لـ int (10)
    /// </summary>
    public int ToInt() => (int)(Value * 100);

    /// <summary>
    /// تحويل لـ string ("10%")
    /// </summary>
    public override string ToString() => $"{ToInt()}%";

    /// <summary>
    /// حساب قيمة من مبلغ
    /// </summary>
    public decimal Calculate(decimal amount)
    {
        return Math.Round(amount * Value, 2, MidpointRounding.AwayFromZero);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
```

---

## 3. Value Objects - Best Practices

### 3.1 الاستخدام في Entities

```csharp
// ❌ خطأ - Properties عادية

public class User
{
    public string Email { get; set; }
    public decimal Balance { get; set; }
    public string PhoneNumber { get; set; }
}

// ✅ صح - Value Objects

public class User
{
    public Email Email { get; private set; }
    public Money Balance { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    
    // Constructor
    private User() { } // EF Core
    
    public User(Email email, Money balance, PhoneNumber phoneNumber)
    {
        Email = email;
        Balance = balance;
        PhoneNumber = phoneNumber;
    }
}
```

### 3.2 Usage Examples

```csharp
// ✅ إنشاء Value Objects

var email = Email.Create("Ahmed@Example.COM");
// email.Value = "ahmed@example.com" ✅会自动 trim + tolower

var money = Money.Create(100.50m, Currency.USD);
// money.Amount = 100.50
// money.Currency = Currency.USD

var address = Address.Create("123 Main St", "Cairo", "Nasr City", "12345", "Egypt");

var phone = PhoneNumber.Create("+20 123 456 789");

// ✅ مقارنة

var email1 = Email.Create("Ahmed@example.com");
var email2 = Email.Create("AHMED@EXAMPLE.COM");
var email3 = Email.Create("Mohamed@example.com");

email1 == email2  // true ✅ (same value)
email1 == email3  // false ✅ (different value)

// ✅ عمليات

var wallet1 = Money.Create(100, Currency.USD);
var wallet2 = Money.Create(50, Currency.USD);

var total = wallet1.Add(wallet2);
// total.Amount = 150
// total.Currency = Currency.USD

// ✅ غلط!

var usdMoney = Money.Create(100, Currency.USD);
var egpMoney = Money.Create(50, Currency.EGP);

var sum = usdMoney.Add(egpMoney);
// ❌ Exception: "Cannot operate on different currencies"

// ✅ تحويل

var usd = Money.Create(100, Currency.USD);
var egp = usd.ConvertTo(Currency.EGP, 30.9m);
// egp.Amount = 3090
// egp.Currency = Currency.EGP

// ✅ Format

var money = Money.Create(1234.50m, Currency.USD);
Console.WriteLine(money.Format());
// "$1,234.50"

var percentage = Percentage.Create(0.15m);
Console.WriteLine(percentage.ToString());
// "15%"
```

### 3.3 EF Core Configuration

```csharp
// DEPI.Infrastructure/Data/Configurations/UserConfiguration.cs

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        // Email as owned type
        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .HasMaxLength(256)
                .IsRequired();
                
            email.HasIndex(e => e.Value).IsUnique();
        });
        
        // Money as owned type
        builder.OwnsOne(u => u.Balance, balance =>
        {
            balance.Property(m => m.Amount)
                .HasColumnName("Balance")
                .HasPrecision(18, 2)
                .IsRequired();
                
            balance.Property(m => m.Currency)
                .HasColumnName("BalanceCurrency")
                .HasConversion<string>()
                .HasMaxLength(3)
                .IsRequired();
        });
        
        // PhoneNumber as owned type
        builder.OwnsOne(u => u.PhoneNumber, phone =>
        {
            phone.Property(p => p.CountryCode)
                .HasColumnName("PhoneCountryCode")
                .HasMaxLength(5)
                .IsRequired();
                
            phone.Property(p => p.Number)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(15)
                .IsRequired();
                
            phone.Property(p => p.FormattedNumber)
                .HasColumnName("PhoneFormatted")
                .HasMaxLength(20)
                .IsRequired();
        });
    }
}
```

### 3.4注意事项

```
✅ DO:

1️⃣ اعمل Value Object immutable
   - private constructor
   - init-only properties (C# 9+)
   - return new instances on changes

2️⃣ استخدم Factory Methods
   - Email.Create()
   - Money.Create()
   - Address.Create()

3️⃣ أضف Validation في Factory
   - Email format validation
   - Money >= 0 validation
   - Address required fields

4️⃣ استخدم TryCreate للتعامل مع errors
   - من غير exceptions
   - TryResult<T>

5️⃣ override Equals + GetHashCode
   - من BaseValueObject
   - للمقارنة + HashSet/Dictionary

6️⃣ استخدم == و != operators
   - للت مقارنة أسهل


❌ DON'T:

1️⃣ لا ت expose الـ constructor
   - هتخلي الناس تعمل objects من غير validation
   - استخدم private constructor + Factory

2️⃣ لا تخلي Properties settable
   - هتخلي الـ Value Object قابل للتعديل
   - استخدم init أو private set

3️⃣ لا تستخدم Value Object لو entity
   - User = entity (لييه ID)
   - Email = value object (مش ليه ID)

4️⃣ لا ترجع null
   - استخدم TryCreate أو throw exception
   - null = source of bugs

5️⃣ لا تقارن بـ == لو مش override
   - لازم ت override == operator
   - لازم ت override GetHashCode()
```

---

# PART 2: DOMAIN EVENTS

## 4. Domain Events - الأساس

### 4.1 إيه هو Domain Event؟

```
🎯 التعريف:
   Domain Event = notification/message إن حاجة حصلت في الـ domain
   "Something happened" = مش "Do something"

📢 مثال من الحقيقي:
   ├── 🔔 "جرس الباب رن" = Event (حصل = رن الجرس)
   ├── 📧 "إيميل وصل" = Event (حصل = وصل إيميل)
   ├── 💰 "فلوس وصلت" = Event (حصل = وصلت فلوس)
   └── 📦 "طلب وصل" = Event (حصل = وصل الطلب)

📦 أمثلة من المشروع:
   ├── UserRegisteredEvent = مستخدم سجل
   ├── ProposalSubmittedEvent = عرض الجديد
   ├── MilestoneCompletedEvent = مرحلة اتكمت
   ├── PaymentReceivedEvent = دفع وصل
   └── ProjectClosedEvent = مشروع اتقفل
```

### 4.2 لية بنستخدم Domain Events؟

```
❌ بدون Events:

public async Task<Result> RegisterUser(RegisterDto dto)
{
    // 1️⃣ Create User
    var user = new User { Email = dto.Email };
    await _repo.Add(user);
    
    // 2️⃣ Create Profile
    var profile = new UserProfile { UserId = user.Id };
    await _repo.AddProfile(profile);
    
    // 3️⃣ Create Wallet
    var wallet = new Wallet { UserId = user.Id, Balance = 0 };
    await _repo.AddWallet(wallet);
    
    // 4️⃣ Send Welcome Email
    await _emailService.SendWelcome(dto.Email);
    
    // 5️⃣ Send Notification to Admin
    await _notificationService.NotifyAdmin($"New user: {dto.Email}");
    
    // 6️⃣ Track Analytics
    await _analytics.Track("user_registered");
    
    // 7️⃣ Setup Default Settings
    await _settingsService.SetupDefaults(user.Id);
    
    // 8️⃣ Create Welcome Task
    await _taskService.CreateWelcomeTask(user.Id);
}

❌ Problems:
   - Use Case = 100+ lines
   - كتير responsibilities
   - صعب test
   - صعب maintain
   - صعب نضيف feature جديدة

✅ مع Events:

public async Task<Result> RegisterUser(RegisterDto dto)
{
    // ✅ 3 سطور بس!
    
    var user = User.Create(dto.Email);
    await _repo.Add(user);
    
    // User.Create() = throws DomainEvent
    
    return Result.Success();
}

// ✅ Handlers (مستقلين):

// Handler 1: Create Profile
public class UserRegisteredEventHandler 
    : IDomainEventHandler<UserRegisteredEvent>
{
    public async Task Handle(UserRegisteredEvent e)
    {
        var profile = new UserProfile { UserId = e.User.Id };
        await _repo.AddProfile(profile);
    }
}

// Handler 2: Create Wallet
public class UserRegisteredEventHandler 
    : IDomainEventHandler<UserRegisteredEvent>
{
    public async Task Handle(UserRegisteredEvent e)
    {
        var wallet = new Wallet { UserId = e.User.Id, Balance = 0 };
        await _repo.AddWallet(wallet);
    }
}

// Handler 3: Send Email
public class UserRegisteredEventHandler 
    : IDomainEventHandler<UserRegisteredEvent>
{
    public async Task Handle(UserRegisteredEvent e)
    {
        await _emailService.SendWelcome(e.User.Email);
    }
}

// Handler 4: Notify Admin
public class UserRegisteredEventHandler 
    : IDomainEventHandler<UserRegisteredEvent>
{
    public async Task Handle(UserRegisteredEvent e)
    {
        await _notificationService.NotifyAdmin($"New user: {e.User.Email}");
    }
}

✅ Benefits:
   - Use Case = 3 lines
   - كل Handler مستقل
   - سهل test
   - سهل maintain
   - سهل نضيف feature جديدة
```

### 4.3 متى بنستخدم Domain Events؟

```
✅ بنستخدم Domain Event لو:

1️⃣ Side Effects مهمة:
   ├── بعد ما مستخدم يسجل = لازم نبعت إيميل
   ├── بعد ما milestone يتكم = لازم ن Notify
   └── بعد ما payment يوصل = لازم ن update الـ wallet

2️⃣ Loose Coupling:
   ├── الـ Use Case = مش بيعرف عن الـ Handlers
   ├── Handlers = مستقلين عن بعض
   └── Infrastructure = منفصل عن Domain

3️⃣ Audit Trail:
   ├── كل Event يتسجل
   ├── بنعرف إيه اللي حصل
   └── بنقدر ن trace

4️⃣ Async Processing:
   ├── Handlers = ممكن تشتغل في background
   ├── مفيش blocking
   └── performance أعلى


❌ مبنستخدمش Domain Event لو:

1️⃣ operation محتاج result فوري:
   ├── لازم الـ email يتبعت قبل ما نرجع response
   └── استخدم messaging/callback

2️⃣ operation simple مش ليه side effects:
   ├── GET /users/1 = مفيش events
   └── CRUD operations بسيطة

3️⃣ Transaction-bound:
   ├── لو الـ side effect لازم يكون في نفس transaction
   └── استخدم mediator/CQRS
```

---

## 5. Domain Events - الكود الصحيح

### 5.1 Base Interfaces

```csharp
// DEPI.Domain/Common/Events/IDomainEvent.cs

namespace DEPI.Domain.Common.Events;

/// <summary>
/// Base interface لكل الـ domain events
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// امتى الـ event حصل
    /// </summary>
    DateTime OccurredAt { get; }
    
    /// <summary>
    /// unique ID للـ event (ل tracing)
    /// </summary>
    Guid EventId { get; }
}

/// <summary>
/// Interface للـ event handler
/// </summary>
public interface IDomainEventHandler<TEvent> 
    where TEvent : IDomainEvent
{
    /// <summary>
    /// لازم يكون async
    /// </summary>
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}
```

### 5.2 Domain Events Registry

```csharp
// DEPI.Domain/Common/Events/DomainEvents.cs

namespace DEPI.Domain.Common.Events;

/// <summary>
///_registry للـ domain events
/// Pattern: Mediator
/// </summary>
public static class DomainEvents
{
    [ThreadStatic]
    private static List<INotification>? _domainEvents;

    /// <summary>
    /// Events اللي حصلت في current thread
    /// </summary>
    public static IReadOnlyCollection<INotification> DomainEventsList
    {
        get
        {
            _domainEvents ??= new List<INotification>();
            return _domainEvents.AsReadOnly();
        }
    }

    /// <summary>
    /// نرفع event
    /// </summary>
    public static void Raise<TEvent>(TEvent @event) 
        where TEvent : IDomainEvent
    {
        _domainEvents ??= new List<INotification>();
        _domainEvents.Add(@event);
    }

    /// <summary>
    /// نمسح الـ events (بعد ما اتprocessed)
    /// </summary>
    public static void Clear()
    {
        _domainEvents?.Clear();
    }
}
```

### 5.3 User Registered Event

```csharp
// DEPI.Domain/Modules/Identity/Events/UserRegisteredEvent.cs

namespace DEPI.Domain.Modules.Identity.Events;

public class UserRegisteredEvent : IDomainEvent
{
    public Guid EventId { get; }
    public DateTime OccurredAt { get; }
    
    public Guid UserId { get; }
    public string Email { get; }
    public string FullName { get; }
    public Enums.UserType UserType { get; }

    public UserRegisteredEvent(
        Guid userId,
        string email,
        string fullName,
        Enums.UserType userType)
    {
        EventId = Guid.NewGuid();
        OccurredAt = DateTime.UtcNow;
        
        UserId = userId;
        Email = email;
        FullName = fullName;
        UserType = userType;
    }
}
```

### 5.4 Proposal Submitted Event

```csharp
// DEPI.Domain/Modules/Projects/Events/ProposalSubmittedEvent.cs

namespace DEPI.Domain.Modules.Projects.Events;

public class ProposalSubmittedEvent : IDomainEvent
{
    public Guid EventId { get; }
    public DateTime OccurredAt { get; }
    
    public Guid ProposalId { get; }
    public Guid ProjectId { get; }
    public Guid FreelancerId { get; }
    public Guid ClientId { get; }
    public decimal ProposedAmount { get; }
    public int DurationDays { get; }

    public ProposalSubmittedEvent(
        Guid proposalId,
        Guid projectId,
        Guid freelancerId,
        Guid clientId,
        decimal proposedAmount,
        int durationDays)
    {
        EventId = Guid.NewGuid();
        OccurredAt = DateTime.UtcNow;
        
        ProposalId = proposalId;
        ProjectId = projectId;
        FreelancerId = freelancerId;
        ClientId = clientId;
        ProposedAmount = proposedAmount;
        DurationDays = durationDays;
    }
}
```

### 5.5 Milestone Completed Event

```csharp
// DEPI.Domain/Modules/Projects/Events/MilestoneCompletedEvent.cs

namespace DEPI.Domain.Modules.Projects.Events;

public class MilestoneCompletedEvent : IDomainEvent
{
    public Guid EventId { get; }
    public DateTime OccurredAt { get; }
    
    public Guid MilestoneId { get; }
    public Guid ContractId { get; }
    public Guid ProjectId { get; }
    public Guid FreelancerId { get; }
    public Guid ClientId { get; }
    public decimal Amount { get; }
    public string MilestoneTitle { get; }
    public bool IsLastMilestone { get; }

    public MilestoneCompletedEvent(
        Guid milestoneId,
        Guid contractId,
        Guid projectId,
        Guid freelancerId,
        Guid clientId,
        decimal amount,
        string milestoneTitle,
        bool isLastMilestone)
    {
        EventId = Guid.NewGuid();
        OccurredAt = DateTime.UtcNow;
        
        MilestoneId = milestoneId;
        ContractId = contractId;
        ProjectId = projectId;
        FreelancerId = freelancerId;
        ClientId = clientId;
        Amount = amount;
        MilestoneTitle = milestoneTitle;
        IsLastMilestone = isLastMilestone;
    }
}
```

### 5.6 Payment Received Event

```csharp
// DEPI.Domain/Modules/Payments/Events/PaymentReceivedEvent.cs

namespace DEPI.Domain.Modules.Payments.Events;

public class PaymentReceivedEvent : IDomainEvent
{
    public Guid EventId { get; }
    public DateTime OccurredAt { get; }
    
    public Guid TransactionId { get; }
    public Guid WalletId { get; }
    public Guid UserId { get; }
    public decimal Amount { get; }
    public string CurrencyCode { get; }
    public Enums.TransactionType TransactionType { get; }
    public string? Description { get; }

    public PaymentReceivedEvent(
        Guid transactionId,
        Guid walletId,
        Guid userId,
        decimal amount,
        string currencyCode,
        Enums.TransactionType transactionType,
        string? description)
    {
        EventId = Guid.NewGuid();
        OccurredAt = DateTime.UtcNow;
        
        TransactionId = transactionId;
        WalletId = walletId;
        UserId = userId;
        Amount = amount;
        CurrencyCode = currencyCode;
        TransactionType = transactionType;
        Description = description;
    }
}
```

### 5.7 Domain Event Dispatcher

```csharp
// DEPI.Infrastructure/Events/DomainEventDispatcher.cs

namespace DEPI.Infrastructure.Events;

/// <summary>
/// Dispatcher للـ domain events
/// بي process الـ events ويحولهم للـ handlers
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    /// dispatch كل الـ events اللي في current request
    /// </summary>
    Task DispatchAsync(CancellationToken cancellationToken = default);
}

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DomainEventDispatcher> _logger;

    public DomainEventDispatcher(
        IServiceProvider serviceProvider,
        ILogger<DomainEventDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task DispatchAsync(CancellationToken cancellationToken = default)
    {
        // جيب كل الـ handlers
        var handlers = _serviceProvider
            .GetServices<IDomainEventHandler>();

        // جيب كل الـ events اللي حصلت
        var events = DomainEvents.DomainEventsList.ToList();

        // امسح الـ events من الـ registry
        DomainEvents.Clear();

        // process كل event
        foreach (var domainEvent in events)
        {
            _logger.LogInformation(
                "Processing domain event: {EventType}",
                domainEvent.GetType().Name);

            try
            {
                // لازم ن cast للـ IDomainEvent
                var @event = (Common.Events.IDomainEvent)domainEvent;
                
                // جيب الـ handler المناسب
                var eventType = @event.GetType();
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
                var handler = _serviceProvider.GetService(handlerType);

                if (handler != null)
                {
                    var handleMethod = handlerType.GetMethod("HandleAsync");
                    var task = (Task?)handleMethod?.Invoke(handler, new[] { @event, cancellationToken });
                    
                    if (task != null)
                        await task;
                        
                    _logger.LogInformation(
                        "Domain event handled successfully: {EventType}",
                        eventType.Name);
                }
                else
                {
                    _logger.LogWarning(
                        "No handler found for domain event: {EventType}",
                        eventType.Name);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error processing domain event: {EventType}",
                    domainEvent.GetType().Name);
                
                // ممكن ن retry أو ن put في dead letter queue
                throw;
            }
        }
    }
}
```

### 5.8 Unit of Work with Events

```csharp
// DEPI.Infrastructure/Data/UnitOfWork.cs

namespace DEPI.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly DepiDbContext _context;
    private readonly IDomainEventDispatcher _eventDispatcher;
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // dispatch events قبل الـ save
        await _eventDispatcher.DispatchAsync(cancellationToken);
        
        return await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // dispatch events قبل الـ save
        await _eventDispatcher.DispatchAsync(cancellationToken);
        
        // dispatch any domain events
        await DispatchDomainEventsAsync();
        
        return true;
    }
    
    private async Task DispatchDomainEventsAsync()
    {
        var domainEntities = _context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(entry => entry.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(entry => entry.Entity.DomainEvents)
            .ToList();

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await _eventDispatcher.DispatchAsync();
        }
    }
}
```

---

## 6. Domain Events - Best Practices

### 6.1 Using Events in Entities

```csharp
// ❌ خطأ - Events مش integrated

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    
    public void ChangeEmail(string newEmail)
    {
        Email = newEmail;
        // ❌ مفيش event!
    }
}

✅ صح - Events integrated

public class User : BaseEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    public string Email { get; private set; }
    
    public void ChangeEmail(string newEmail)
    {
        if (Email == newEmail)
            return;
            
        Email = newEmail;
        
        // ✅ raise event
        AddDomainEvent(new UserEmailChangedEvent(Id, newEmail));
    }
    
    public void ClearDomainEvents() => _domainEvents.Clear();
    
    private void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }
}
```

### 6.2 Raising Events from Factory

```csharp
// ✅ صح - Event من Factory

public class User : BaseEntity
{
    public string Email { get; }
    public string FullName { get; }
    
    private User() { } // EF Core
    
    public static User Create(string email, string fullName)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            FullName = fullName,
            CreatedAt = DateTime.UtcNow
        };
        
        // ✅ raise event
        DomainEvents.Raise(new UserRegisteredEvent(
            user.Id,
            user.Email,
            user.FullName));
        
        return user;
    }
}
```

### 6.3 Event Handlers Examples

```csharp
// Handler 1: Create Profile

public class UserRegisteredEventHandler 
    : IDomainEventHandler<UserRegisteredEvent>
{
    private readonly IUserRepository _userRepository;
    
    public async Task HandleAsync(
        UserRegisteredEvent @event, 
        CancellationToken cancellationToken)
    {
        var profile = new UserProfile
        {
            Id = Guid.NewGuid(),
            UserId = @event.UserId,
            CreatedAt = DateTime.UtcNow
        };
        
        await _userRepository.AddProfileAsync(profile);
    }
}

// Handler 2: Create Wallet

public class UserRegisteredEventHandler 
    : IDomainEventHandler<UserRegisteredEvent>
{
    private readonly IWalletRepository _walletRepository;
    
    public async Task HandleAsync(
        UserRegisteredEvent @event, 
        CancellationToken cancellationToken)
    {
        var wallet = new Wallet
        {
            Id = Guid.NewGuid(),
            OwnerUserId = @event.UserId,
            Balance = 0,
            CurrencyCode = "USD",
            Status = WalletStatus.Active,
            CreatedAt = DateTime.UtcNow
        };
        
        await _walletRepository.AddAsync(wallet);
    }
}

// Handler 3: Send Welcome Email

public class UserRegisteredEventHandler 
    : IDomainEventHandler<UserRegisteredEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<UserRegisteredEventHandler> _logger;
    
    public async Task HandleAsync(
        UserRegisteredEvent @event, 
        CancellationToken cancellationToken)
    {
        try
        {
            await _emailService.SendWelcomeEmailAsync(
                @event.Email,
                @event.FullName);
                
            _logger.LogInformation(
                "Welcome email sent to {Email}", 
                @event.Email);
        }
        catch (Exception ex)
        {
            // ❌ مبنرفضش الـ transaction كله
            // بس بن log الـ error
            _logger.LogError(
                ex,
                "Failed to send welcome email to {Email}",
                @event.Email);
        }
    }
}

// Handler 4: Notify Admin

public class UserRegisteredEventHandler 
    : IDomainEventHandler<UserRegisteredEvent>
{
    private readonly INotificationService _notificationService;
    private readonly IUserRepository _userRepository;
    
    public async Task HandleAsync(
        UserRegisteredEvent @event, 
        CancellationToken cancellationToken)
    {
        var admins = await _userRepository.GetAdminsAsync();
        
        foreach (var admin in admins)
        {
            await _notificationService.SendAsync(
                admin.Id,
                "New User Registered",
                $"A new {@event.UserType} has registered: {@event.FullName} ({@event.Email})",
                NotificationType.UserRegistered);
        }
    }
}

// Handler 5: Track Analytics

public class UserRegisteredEventHandler 
    : IDomainEventHandler<UserRegisteredEvent>
{
    private readonly IAnalyticsService _analyticsService;
    
    public async Task HandleAsync(
        UserRegisteredEvent @event, 
        CancellationToken cancellationToken)
    {
        await _analyticsService.TrackAsync(
            "user_registered",
            new Dictionary<string, object>
            {
                ["userId"] = @event.UserId,
                ["email"] = @event.Email,
                ["userType"] = @event.UserType.ToString(),
                ["timestamp"] = @event.OccurredAt
            });
    }
}
```

### 6.4注意事项

```
✅ DO:

1️⃣ اسم الـ Event يوصف الـ action
   ├── UserRegisteredEvent ✅
   ├── ProposalSubmittedEvent ✅
   └── PaymentReceivedEvent ✅

2️⃣ استخدم past tense للـ naming
   ├── Registered ✅ (past)
   ├── Submitted ✅ (past)
   └── Completed ✅ (past)

3️⃣ include relevant data in event
   ├── UserId
   ├── Email
   └── Timestamp

4️⃣ make events immutable
   ├── readonly properties
   └── private constructor

5️⃣ handle errors gracefully in handlers
   ├── log errors
   ├── don't fail the entire transaction
   └── use retry patterns

6️⃣ dispatch events after save
   └── use Unit of Work pattern


❌ DON'T:

1️⃣ لا تسمي event بـ "Handle" أو "Process"
   ├── UserRegisteredHandler ❌
   └── ProcessUserRegistration ❌

2️⃣ لا ت include sensitive data
   ├── PasswordHash ❌
   └── CreditCardNumber ❌

3️⃣ لا ترجع result من handlers
   ├── handlers = void (fire-and-forget)
   └── لو محتاج result = استخدم CQRS/Mediator

4️⃣ لا تعمل blocking في handlers
   ├── handlers = async
   └── use message queue للـ heavy processing

5️⃣ لا تعتمد على order من handlers
   ├── handlers = مستقلين
   └── لو محتاج order = use Saga/CQRS
```

---

## 7. Integration - Value Objects + Events مع بعض

### 7.1 User Entity مع Value Objects + Events

```csharp
// DEPI.Domain/Modules/Identity/Entities/User.cs

namespace DEPI.Domain.Modules.Identity.Entities;

public class User : BaseEntity
{
    // ✅ Value Objects
    public Email Email { get; private set; } = null!;
    public FullName FullName { get; private set; } = null!;
    public PhoneNumber? PhoneNumber { get; private set; }
    
    // ✅ Value Object
    private HashedPassword _passwordHash = null!;
    public HashedPassword PasswordHash => _passwordHash;
    
    // Regular Properties
    public UserType UserType { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsEmailVerified { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    
    // Events
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    // EF Core
    private User() { }

    #region Factory

    /// <summary>
    /// ✅ Factory Method - لازم نستخدم ده بدل new
    /// </summary>
    public static User Create(
        Email email,
        FullName fullName,
        HashedPassword passwordHash,
        UserType userType = UserType.Freelancer)
    {
        if (email == null)
            throw new ArgumentNullException(nameof(email));
            
        if (fullName == null)
            throw new ArgumentNullException(nameof(fullName));
            
        if (passwordHash == null)
            throw new ArgumentNullException(nameof(passwordHash));

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            FullName = fullName,
            _passwordHash = passwordHash,
            UserType = userType,
            IsActive = true,
            IsEmailVerified = false,
            CreatedAt = DateTime.UtcNow
        };

        // ✅ Raise Domain Event
        user.AddDomainEvent(new UserRegisteredEvent(
            user.Id,
            user.Email.Value,
            user.FullName.FullNameFormatted,
            user.UserType));

        return user;
    }

    #endregion

    #region Methods

    public void Activate()
    {
        if (IsActive)
            return;
            
        IsActive = true;
        AddDomainEvent(new UserActivatedEvent(Id));
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;
            
        IsActive = false;
        AddDomainEvent(new UserDeactivatedEvent(Id));
    }

    public void UpdateEmail(Email newEmail)
    {
        if (Email == newEmail)
            return;
            
        var oldEmail = Email;
        Email = newEmail;
        
        AddDomainEvent(new UserEmailChangedEvent(Id, oldEmail.Value, newEmail.Value));
    }

    public void UpdateFullName(FullName newFullName)
    {
        if (FullName == newFullName)
            return;
            
        FullName = newFullName;
        AddDomainEvent(new UserFullNameChangedEvent(Id, newFullName.FullNameFormatted));
    }

    public void ChangePassword(HashedPassword newPasswordHash)
    {
        if (_passwordHash == newPasswordHash)
            return;
            
        _passwordHash = newPasswordHash;
        AddDomainEvent(new UserPasswordChangedEvent(Id));
    }

    public void RecordLogin()
    {
        LastLoginAt = DateTime.UtcNow;
        AddDomainEvent(new UserLoggedInEvent(Id));
    }

    public void VerifyEmail()
    {
        if (IsEmailVerified)
            return;
            
        IsEmailVerified = true;
        AddDomainEvent(new UserEmailVerifiedEvent(Id));
    }

    #endregion

    #region Domain Events

    private void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    #endregion
}
```

### 7.2 Proposal Entity مع Value Objects + Events

```csharp
// DEPI.Domain/Modules/Projects/Entities/Proposal.cs

namespace DEPI.Domain.Modules.Projects.Entities;

public class Proposal : BaseEntity
{
    // ✅ Value Objects
    public Money ProposedAmount { get; private set; } = null!;
    
    // Regular Properties
    public Guid ProjectId { get; private set; }
    public Guid FreelancerId { get; private set; }
    public string CoverLetter { get; private set; } = string.Empty;
    public int DurationDays { get; private set; }
    public ProposalStatus Status { get; private set; }
    
    // Events
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    // Navigation
    public virtual Project? Project { get; private set; }
    public virtual Freelancer? Freelancer { get; private set; }
    public virtual ICollection<Milestone> Milestones { get; } = new List<Milestone>();

    // EF Core
    private Proposal() { }

    #region Factory

    public static Proposal Create(
        Guid projectId,
        Guid freelancerId,
        Money proposedAmount,
        string coverLetter,
        int durationDays)
    {
        if (projectId == Guid.Empty)
            throw new ArgumentException("ProjectId is required", nameof(projectId));
            
        if (freelancerId == Guid.Empty)
            throw new ArgumentException("FreelancerId is required", nameof(freelancerId));
            
        if (proposedAmount == null)
            throw new ArgumentNullException(nameof(proposedAmount));
            
        if (string.IsNullOrWhiteSpace(coverLetter))
            throw new ArgumentException("Cover letter is required", nameof(coverLetter));
            
        if (durationDays <= 0)
            throw new ArgumentException("Duration must be positive", nameof(durationDays));

        var proposal = new Proposal
        {
            Id = Guid.NewGuid(),
            ProjectId = projectId,
            FreelancerId = freelancerId,
            ProposedAmount = proposedAmount,
            CoverLetter = coverLetter.Trim(),
            DurationDays = durationDays,
            Status = ProposalStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        // ✅ Raise Domain Event
        proposal.AddDomainEvent(new ProposalSubmittedEvent(
            proposal.Id,
            projectId,
            freelancerId,
            proposal.ProposedAmount.Amount,
            durationDays));

        return proposal;
    }

    #endregion

    #region Methods

    public void Shortlist()
    {
        if (Status != ProposalStatus.Pending)
            throw new InvalidOperationException("Only pending proposals can be shortlisted");
            
        Status = ProposalStatus.Shortlisted;
        AddDomainEvent(new ProposalShortlistedEvent(Id, ProjectId, FreelancerId));
    }

    public void Reject()
    {
        if (Status == ProposalStatus.Rejected)
            return;
            
        Status = ProposalStatus.Rejected;
        AddDomainEvent(new ProposalRejectedEvent(Id, ProjectId, FreelancerId));
    }

    public void Withdraw()
    {
        if (Status == ProposalStatus.Withdrawn || Status == ProposalStatus.Accepted)
            throw new InvalidOperationException("Cannot withdraw this proposal");
            
        Status = ProposalStatus.Withdrawn;
        AddDomainEvent(new ProposalWithdrawnEvent(Id, ProjectId, FreelancerId));
    }

    #endregion

    #region Domain Events

    private void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    #endregion
}
```

### 7.3 Wallet Entity مع Value Objects + Events

```csharp
// DEPI.Domain/Modules/Payments/Entities/Wallet.cs

namespace DEPI.Domain.Modules.Payments.Entities;

public class Wallet : BaseEntity
{
    // ✅ Value Objects
    public Money Balance { get; private set; } = null!;
    public Money PendingBalance { get; private set; } = null!;
    
    // Regular Properties
    public Guid OwnerUserId { get; private set; }
    public WalletStatus Status { get; private set; }
    
    // Events
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    // Navigation
    public virtual User? OwnerUser { get; private set; }

    // EF Core
    private Wallet() { }

    #region Factory

    public static Wallet Create(Guid ownerUserId, Currency currency = Currency.USD)
    {
        if (ownerUserId == Guid.Empty)
            throw new ArgumentException("OwnerUserId is required", nameof(ownerUserId));

        return new Wallet
        {
            Id = Guid.NewGuid(),
            OwnerUserId = ownerUserId,
            Balance = Money.Zero(currency),
            PendingBalance = Money.Zero(currency),
            Status = WalletStatus.Active,
            CreatedAt = DateTime.UtcNow
        };
    }

    #endregion

    #region Operations

    public void Deposit(Money amount, string description)
    {
        if (amount == null)
            throw new ArgumentNullException(nameof(amount));
            
        if (!amount.IsPositive)
            throw new ArgumentException("Deposit amount must be positive", nameof(amount));
            
        if (Status != WalletStatus.Active)
            throw new InvalidOperationException("Wallet is not active");

        Balance = Balance.Add(amount);
        
        AddDomainEvent(new MoneyDepositedEvent(
            Id,
            OwnerUserId,
            amount.Amount,
            amount.Currency.Code,
            description));
    }

    public void AddToPending(Money amount)
    {
        if (amount == null || !amount.IsPositive)
            throw new ArgumentException("Amount must be positive", nameof(amount));
            
        PendingBalance = PendingBalance.Add(amount);
    }

    public void MoveFromPendingToAvailable(Money amount)
    {
        if (amount == null || !amount.IsPositive)
            throw new ArgumentException("Amount must be positive", nameof(amount));
            
        if (PendingBalance < amount)
            throw new InvalidOperationException("Insufficient pending balance");

        PendingBalance = PendingBalance.Subtract(amount);
        Balance = Balance.Add(amount);
        
        AddDomainEvent(new MoneyReleasedEvent(
            Id,
            OwnerUserId,
            amount.Amount,
            amount.Currency.Code));
    }

    public void Withdraw(Money amount, string description)
    {
        if (amount == null)
            throw new ArgumentNullException(nameof(amount));
            
        if (!amount.IsPositive)
            throw new ArgumentException("Withdrawal amount must be positive", nameof(amount));
            
        if (Status != WalletStatus.Active)
            throw new InvalidOperationException("Wallet is not active");
            
        if (Balance < amount)
            throw new InvalidOperationException("Insufficient balance");

        Balance = Balance.Subtract(amount);
        
        AddDomainEvent(new MoneyWithdrawnEvent(
            Id,
            OwnerUserId,
            amount.Amount,
            amount.Currency.Code,
            description));
    }

    public void Freeze()
    {
        if (Status == WalletStatus.Frozen)
            return;
            
        Status = WalletStatus.Frozen;
        AddDomainEvent(new WalletFrozenEvent(Id, OwnerUserId));
    }

    #endregion

    #region Domain Events

    private void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    #endregion
}
```

---

## 8. Real Project Examples

### 8.1 Complete Use Case with Events

```csharp
// DEPI.Application/UseCases/Identity/RegisterUseCase.cs

namespace DEPI.Application.UseCases.Identity;

public class RegisterUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<AppResult<AuthResponseDto>> Handle(RegisterDto dto)
    {
        // 1️⃣ Validate
        var emailResult = Email.TryCreate(dto.Email);
        if (emailResult.IsFailure)
            return AppResult<AuthResponseDto>.Failure(emailResult.Error!);

        var fullNameResult = FullName.TryCreate(dto.FirstName, dto.LastName);
        if (fullNameResult.IsFailure)
            return AppResult<AuthResponseDto>.Failure(fullNameResult.Error!);

        // 2️⃣ Check Duplicate
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            return AppResult<AuthResponseDto>.Failure("Email already exists");

        // 3️⃣ Hash Password
        var passwordHash = _passwordHasher.Hash(dto.Password);

        // 4️⃣ Create User (with Events!)
        var user = User.Create(
            emailResult.Value,
            fullNameResult.Value,
            passwordHash,
            dto.UserType);

        // 5️⃣ Save
        await _userRepository.AddAsync(user);
        
        // ✅ Events会自动 dispatched في SaveChangesAsync
        await _unitOfWork.SaveChangesAsync();

        // 6️⃣ Generate Token
        var token = await _tokenService.GenerateAccessTokenAsync(user.Id);

        return AppResult<AuthResponseDto>.Success(new AuthResponseDto
        {
            AccessToken = token,
            User = MapToDto(user)
        });
    }
}
```

### 8.2 EF Core Configuration for Value Objects

```csharp
// DEPI.Infrastructure/Data/Configurations/WalletConfiguration.cs

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets");

        builder.HasKey(w => w.Id);

        // ✅ Money Value Objects as Owned Types
        builder.OwnsOne(w => w.Balance, balance =>
        {
            balance.Property(m => m.Amount)
                .HasColumnName("Balance")
                .HasPrecision(18, 2)
                .IsRequired();
                
            balance.Property(m => m.Currency)
                .HasColumnName("BalanceCurrency")
                .HasConversion<string>()
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.OwnsOne(w => w.PendingBalance, pending =>
        {
            pending.Property(m => m.Amount)
                .HasColumnName("PendingBalance")
                .HasPrecision(18, 2)
                .IsRequired();
                
            pending.Property(m => m.Currency)
                .HasColumnName("PendingBalanceCurrency")
                .HasConversion<string>()
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.Property(w => w.OwnerUserId)
            .IsRequired();

        builder.Property(w => w.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(w => w.OwnerUser)
            .WithOne(u => u.Wallet)
            .HasForeignKey<Wallet>(w => w.OwnerUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

### 8.3 Event Handlers with DI

```csharp
// DEPI.Infrastructure/Events/EventHandlersRegistration.cs

namespace DEPI.Infrastructure.Events;

public static class EventHandlersRegistration
{
    public static IServiceCollection AddDomainEventHandlers(
        this IServiceCollection services)
    {
        // ✅ Register all handlers
        services.AddScoped<IDomainEventHandler<UserRegisteredEvent>, UserRegistered_CreateProfileHandler>();
        services.AddScoped<IDomainEventHandler<UserRegisteredEvent>, UserRegistered_CreateWalletHandler>();
        services.AddScoped<IDomainEventHandler<UserRegisteredEvent>, UserRegistered_SendWelcomeEmailHandler>();
        services.AddScoped<IDomainEventHandler<UserRegisteredEvent>, UserRegistered_NotifyAdminHandler>();
        services.AddScoped<IDomainEventHandler<UserRegisteredEvent>, UserRegistered_TrackAnalyticsHandler>();

        services.AddScoped<IDomainEventHandler<ProposalSubmittedEvent>, ProposalSubmitted_NotifyClientHandler>();
        services.AddScoped<IDomainEventHandler<ProposalSubmittedEvent>, ProposalSubmitted_IncrementCounterHandler>();

        services.AddScoped<IDomainEventHandler<MilestoneCompletedEvent>, MilestoneCompleted_AddToPendingHandler>();
        services.AddScoped<IDomainEventHandler<MilestoneCompletedEvent>, MilestoneCompleted_NotifyFreelancerHandler>();
        services.AddScoped<IDomainEventHandler<MilestoneCompletedEvent>, MilestoneCompleted_NotifyClientHandler>();

        services.AddScoped<IDomainEventHandler<PaymentReceivedEvent>, PaymentReceived_UpdateWalletHandler>();
        services.AddScoped<IDomainEventHandler<PaymentReceivedEvent>, PaymentReceived_SendReceiptHandler>();
        services.AddScoped<IDomainEventHandler<PaymentReceivedEvent>, PaymentReceived_TrackAnalyticsHandler>();

        return services;
    }
}
```

---

## Summary

```
┌─────────────────────────────────────────────────────────────────┐
│                     VALUE OBJECTS                                │
│                                                                 │
│  🎯 "شيء" لازم يتوصف بـ values                                │
│                                                                 │
│  ✅ Immutable                                                │
│  ✅ Type Safety                                              │
│  ✅ Validation داخلي                                          │
│  ✅ Comparability                                            │
│                                                                 │
│  📦 Examples:                                                 │
│     ├── Email("ahmed@example.com")                           │
│     ├── Money(100, Currency.USD)                             │
│     ├── Address("123 Main St", "Cairo", "Egypt")             │
│     └── PhoneNumber("+20", "123456789")                     │
│                                                                 │
│  📝 Code Pattern:                                             │
│     public class Email : ValueObject                          │
│     {                                                         │
│         public string Value { get; }                           │
│         private Email(string value) { Value = value; }         │
│         public static Email Create(string email)               │
│         protected override IEnumerable GetEqualityComponents()  │
│     }                                                         │
└─────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│                     DOMAIN EVENTS                                │
│                                                                 │
│  🎯 "خبر" إن حاجة حصلت                                        │
│                                                                 │
│  ✅ Loose Coupling                                           │
│  ✅ Single Responsibility                                     │
│  ✅ Extensibility                                             │
│  ✅ Audit Trail                                              │
│                                                                 │
│  📦 Examples:                                                 │
│     ├── UserRegisteredEvent(userId, email)                    │
│     ├── ProposalSubmittedEvent(proposalId, amount)            │
│     └── MilestoneCompletedEvent(milestoneId, amount)          │
│                                                                 │
│  📝 Code Pattern:                                             │
│     public class UserRegisteredEvent : IDomainEvent           │
│     {                                                         │
│         public Guid UserId { get; }                           │
│         public string Email { get; }                           │
│         public DateTime OccurredAt { get; }                   │
│     }                                                         │
│                                                                 │
│     // In Entity:                                              │
│     public static User Create(...)                             │
│     {                                                         │
│         var user = new User();                                │
│         DomainEvents.Raise(new UserRegisteredEvent(...));     │
│         return user;                                          │
│     }                                                         │
└─────────────────────────────────────────────────────────────────┘
```

---

*Document created: April 2026*
*Project: DEPI Smart Freelance Platform*
*Architecture: Clean Architecture - Value Objects & Domain Events*
