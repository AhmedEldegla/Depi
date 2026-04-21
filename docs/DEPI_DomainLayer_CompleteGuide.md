# Domain Layer - شرح شامل ومفصل

## قائمة المحتويات

1. [مقدمة في Clean Architecture](#1-مقدمة-في-clean-architecture)
2. [Domain Layer - ايه هو؟](#2-domain-layer---ايه-هو)
3. [Base Classes - الكلاسات الاساسية](#3-base-classes---الكلاسات-الاساسية)
4. [Entities - الكيانات](#4-entities---الكيانات)
5. [Value Objects - كائنات القيمة](#5-value-objects---كائنات-القيمة)
6. [Enums - التعدادات](#6-enums---التعدادات)
7. [Interfaces - الواجهات](#7-interfaces---الواجهات)
8. [Domain Events - احداث الدومين](#8-domain-events---احداث-الدومين)
9. [Relationships - العلاقات](#9-relationships---العلاقات)
10. [نصائح مهمة](#10-نصائح-مهمة)

---

## 1. مقدمة في Clean Architecture

### ايه هي Clean Architecture؟

دي طريقة لتنظيم الكود بتخليه سهل في التعديل والصيانة. الفكرة ان الكود بتاعك يكون:

- **Independent of frameworks** - مش مرتبط باطار عمل معين
- **Testable** - سهل تتعمله Tests
- **Independent of UI** - UI منفصل عن المنطق
- **Independent of database** - ممكن تبدل الداتابيز من غير ما تكسر الكود
- **Independent of any external agency** - مش مرتبط بخدمات خارجية

### الطبقات (Layers)

```
┌─────────────────────────────────────────────────────┐
│                     API Layer                        │
│              (Controllers, Endpoints)                 │
│              بيتقبل الطلبات ويرجع النتائج              │
└──────────────────────────┬──────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────┐
│                  Application Layer                   │
│                    (UseCases)                         │
│              فيها منطق الشغل - تعمل ايه                │
└──────────────────────────┬──────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────┐
│                    Domain Layer                      │
│                   (Entities, Enums)                   │
│             فيها منطق الشغل - بتمثل ايه               │
│             (القواعد اللي تحكم البيانات)                │
└──────────────────────────┬──────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────┐
│                Infrastructure Layer                   │
│              (Database, External Services)            │
│               التنفيذ الفعلي - ازاي نعمل باه            │
└─────────────────────────────────────────────────────┘
```

---

## 2. Domain Layer - ايه هو؟

### التعريف

Domain Layer هو قلب التطبيق. دي الطبقة اللي بتحتوي على:

1. **Business Logic** - منطق الشغل الاساسي
2. **Business Rules** - القواعد اللي بتحكم الشغل
3. **Business Entities** - الكيانات اللي بتوصف الشغل
4. **Domain Events** - الاحداث اللي بتوصلح اثناء الشغل

### ايه اللي مش موجود في Domain Layer؟

- **ما فيش** Http Requests او Responses
- **ما فيش** Database operations
- **ما فيش** Dependency Injection
- **ما فيش** frameworkspecific code

### الفكرة باختصار

> Domain Layer بيمثل **"ايه"** - يعني الشغل اللي عندنا بيمثل ايه
> Application Layer بيمثل **"لية"** - يعني عايزين نعمل باه ايه
> Infrastructure Layer بيمثل **"ازاي"** - يعني هنعمل باه ازاي

---

## 3. Base Classes - الكلاسات الاساسية

### 3.1 BaseEntity - الكيان الاساسي

دي اول كلاس لازم تعرفه. كل كيان في المشروع بيرث منها.

**الموقع:** `src/DEPI.Domain/Common/Base/BaseEntity.cs`

```csharp
namespace DEPI.Domain.Common.Base;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
}
```

**ازاي نستخدمها:**

```csharp
// كل Entity بتاعك لازم يورث من BaseEntity
public class User : BaseEntity
{
    public string Email { get; set; }
    // ...
}

public class Project : BaseEntity
{
    public string Title { get; set; }
    // ...
}
```

**لية نستخدمها؟**

عشان كل الكيانات ليها `Id` موحد. بدل ما نكتب `Id` في كل كيان، بنورث من `BaseEntity`.

---

### 3.2 AuditableEntity - الكيان القابل للتتبع

دي بتمتد من `BaseEntity` وبتضيف حقول للتتبع.

**الموقع:** `src/DEPI.Domain/Common/Base/AuditableEntity.cs`

```csharp
namespace DEPI.Domain.Common.Base;

public abstract class AuditableEntity : BaseEntity
{
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}
```

### شرح الحقول:

| الحقل | الوصف |
|-------|-------|
| `CreatedBy` | مين اللي عمل الكيان دا |
| `CreatedAt` | امتى اتعمل الكيان |
| `UpdatedBy` | مين اللي عدله |
| `UpdatedAt` | امتى اتعدل |
| `DeletedBy` | مين اللي مسحه |
| `DeletedAt` | امتى اتمسح |
| `IsDeleted` | هل الكيان متمسح ولا لا |

**ازاي نستخدمها:**

```csharp
// الكيانات اللي عايزين نتبعها بنورث من AuditableEntity
public class User : AuditableEntity
{
    public string Email { get; set; }
    public string FullName { get; set; }
    // ...
}
```

---

## 4. Entities - الكيانات

### 4.1 ايه هو Entity؟

Entity هو كائن له هوية فريدة. يعني حتى لو كل بياناته زي كيان تاني، هو يختلف عنه بالـ Id.

### مثال من الكود:

**الموقع:** `src/DEPI.Domain/Modules/Identity/Entities/User.cs`

```csharp
using System.ComponentModel.DataAnnotations;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Enums;
using DEPI.Domain.Modules.References.Entities;

namespace DEPI.Domain.Modules.Identity.Entities;

public class User : AuditableEntity
{
    // ====== Required Fields - الحقول المطلوبة ======

    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string FullName { get; set; } = string.Empty;

    // ====== Enums - التعدادات ======
    public UserType UserType { get; set; }

    // ====== Optional Fields - الحقول الاختيارية ======
    public int? CountryId { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsEmailVerified { get; set; }
    public DateTime? LastLoginAt { get; set; }

    // ====== Refresh Token Fields ======
    [MaxLength(500)]
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }

    // ====== Navigation Properties - خصائص التنقل ======
    public virtual Country? Country { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    public virtual ICollection<SecurityLog> SecurityLogs { get; set; } = new List<SecurityLog>();
    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();
}
```

---

### 4.2 شرح اجزاء Entity:

#### A. Data Annotations - البيانات الوصفية

دي ملاحظات بتحدد قواعد البيانات:

```csharp
[Required]              // لازم يكون موجود - مش فاضي
[EmailAddress]          // لازم يكون Email صحيح
[MaxLength(256)]        // اقصي طول 256 حرف
[MinLength(8)]          // اقل طول 8 حروف
[StringLength(100)]     // الطول بين كذا وكذا
[Range(1, 100)]         // القيمة بين 1 و 100
[Phone]                 // لازم يكون رقم تليفون صحيح
[Url]                   // لازم يكون رابط صحيح
```

#### B. Types - الانواع

```csharp
public string Email { get; set; }                    // نص
public string PasswordHash { get; set; }             // نص مشفر
public string FullName { get; set; } = string.Empty; // نص مع قيمة افتراضية
public UserType UserType { get; set; }               // Enum
public int? CountryId { get; set; }                  // رقم (nullable)
public bool IsActive { get; set; } = true;          // صح او غلط مع قيمة افتراضية
public DateTime? LastLoginAt { get; set; }          // تاريخ ووقت (nullable)
public Guid Id { get; set; }                         // معرف فريد
```

#### C. Navigation Properties - خصائص التنقل

دي بتربط الكيانات ببعض:

```csharp
public virtual Country? Country { get; set; }
// Entity واحد بيربط ب entity تاني

public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
// Entity واحد بيربط باكتر من entity تاني
```

---

### 4.3 Examples - امثلة من الكود:

#### User Entity - مستخدم

```csharp
public class User : AuditableEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string FullName { get; set; }
    public UserType UserType { get; set; }
    public bool IsActive { get; set; }
    public bool IsEmailVerified { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}
```

#### Project Entity - مشروع

```csharp
public class Project : AuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Budget { get; set; }
    public ProjectStatus Status { get; set; }
    public Guid OwnerId { get; set; }
    public Guid? FreelancerId { get; set; }
}
```

#### Wallet Entity - محفظة

```csharp
public class Wallet : BaseEntity
{
    public Guid OwnerUserId { get; set; }
    public decimal Balance { get; set; }
    public decimal PendingBalance { get; set; }
    public string CurrencyCode { get; set; }
    public WalletStatus Status { get; set; }
}
```

---

## 5. Value Objects - كائنات القيمة

### 5.1 ايه هو Value Object؟

Value Object هو كائن بدون هوية فريدة. قيمته تتحدد بمحتوياته.

### الفرق بين Entity و Value Object:

| Entity | Value Object |
|--------|-------------|
| له هوية فريدة (Id) | ما لهش هوية |
| بيتغير (mutable) | ما بيتغيرش (immutable) |
| بيتقارن بالـ Id | بيتقارن بالمحتوى |

### مثال:

```
Entity:        User (Id=1) ≠ User (Id=2)          // حتى لو الاسم نفسو
Value Object:  Money(100, USD) = Money(100, USD)  // القيم متساوية
```

---

### 5.2 Base Value Object - كلاس اساسي

**الموقع:** `src/DEPI.Domain/Common/ValueObjects/ValueObject.cs`

```csharp
namespace DEPI.Domain.Common.ValueObjects;

public abstract class ValueObject
{
    // دي Methode بتجيب القيم اللي بنقارن بيها
    protected abstract IEnumerable<object> GetEqualityComponents();

    // بنعدل طريقة المقارنة
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        var valueObject = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    // بنعدل طريقة حساب الهاش
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    // عمليات المقارنة
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
        => !(left == right);
}
```

---

### 5.3 امثلة عملية:

#### Email Value Object:

```csharp
public class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLower();
    }

    // Factory Method - طريقة امنة لإنشاء Email
    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));

        if (!email.Contains("@"))
            throw new ArgumentException("Invalid email format", nameof(email));

        return new Email(email);
    }

    // Factory Method - لو عايز من غير Exception
    public static bool TryCreate(string email, out Email? result)
    {
        result = null;
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            return false;

        result = new Email(email);
        return true;
    }
}
```

**الاستخدام:**

```csharp
// الطريقة التقليدية (string)
public string Email { get; set; }  // مش متأكد من الصيغة

// الطريقة بـ Value Object
public Email Email { get; set; }    // متأكد من الصيغة
```

#### Money Value Object:

```csharp
public class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    // Factory Method
    public static Money Create(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative");

        return new Money(amount, currency);
    }

    // عمليات حسابية
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Cannot add different currencies");

        return new Money(Amount + other.Amount, Currency);
    }

    public Money Multiply(int multiplier)
    {
        return new Money(Amount * multiplier, Currency);
    }
}
```

---

### 5.4 امتى نستخدم Value Objects؟

| استخدم | ما تستخدمش |
|--------|-----------|
| Email, Phone, Address | Id, Name, Description |
| Money, Percentage, Coordinates | Entity References |
| DateRange, TimePeriod | Dates alone |
| Password (مع validation معقد) | Passwords البسيطة |

---

## 6. Enums - التعدادات

### 6.1 ايه هو Enum؟

Enum هو نوع بياخد قيم ثابتة ومحددة.

### مثال:

```csharp
public enum UserType
{
    Freelancer = 1,    // حرفي
    Client = 2,        // عميل
    Admin = 3          // مدير
}
```

---

### 6.2 امثلة من الكود:

#### UserType - نوع المستخدم

```csharp
namespace DEPI.Domain.Modules.Identity.Enums;

public enum UserType
{
    Freelancer = 1,
    Client = 2,
    Admin = 3
}
```

#### VerificationStatus - حالة التحقق

```csharp
namespace DEPI.Domain.Modules.Identity.Enums;

public enum VerificationStatus
{
    Pending = 1,
    InReview = 2,
    Approved = 3,
    Rejected = 4
}
```

#### DocumentType - نوع المستند

```csharp
namespace DEPI.Domain.Modules.Identity.Enums;

public enum DocumentType
{
    NationalId = 1,
    Passport = 2,
    DriverLicense = 3
}
```

#### ProjectStatus - حالة المشروع

```csharp
namespace DEPI.Domain.Modules.Projects.Enums;

public enum ProjectStatus
{
    Draft = 1,
    Open = 2,
    InProgress = 3,
    Completed = 4,
    Cancelled = 5
}
```

---

### 6.3 ازاي نستخدم Enum في Entity:

```csharp
public class User : AuditableEntity
{
    public string Email { get; set; }
    public UserType UserType { get; set; }  // استخدام Enum
}
```

**في الـ Database:** بيتخزن كرقم (int)
**في الكود:** بتستخدم كـ enum مع اسم واضح

---

## 7. Interfaces - الواجهات

### 7.1 ايه هو Interface؟

Interface هو عقد بيحدد Methods اللي لازم تتنفذ. ما فيهش تنفيذ، بس بنقول "اللي هستخدمه لازم يimplement methods دي".

### 7.2 مثال من الكود:

#### IUserRepository

**الموقع:** `src/DEPI.Domain/Shared/Interfaces/IUserRepository.cs`

```csharp
using DEPI.Domain.Modules.Identity.Entities;
using DEPI.Domain.Modules.Payments.Entities;
using DEPI.Domain.Modules.Profiles.Entities;

namespace DEPI.Domain.Shared.Interfaces;

public interface IUserRepository
{
    // ====== Basic CRUD Operations ======

    Task<User?> GetByIdAsync(Guid id);
    // Method: جيب مستخدم بالـ Id
    // Return: User ولا null لو مش موجود

    Task<User?> GetByEmailAsync(string email);
    // Method: جيب مستخدم بالـ Email
    // Return: User ولا null

    Task<User?> GetByRefreshTokenAsync(string refreshToken);
    // Method: جيب مستخدم بالـ Refresh Token
    // Return: User ولا null

    Task<IEnumerable<User>> GetAllAsync();
    // Method: جيب كل المستخدمين
    // Return: قائمة بالمستخدمين

    Task AddAsync(User user);
    // Method: اضافة مستخدم جديد
    // Return: حاجة - بس بتعمل save

    Task UpdateAsync(User user);
    // Method: تحديث مستخدم موجود
    // Return: حاجة - بس بتعمل save

    Task DeleteAsync(Guid id);
    // Method: مسح مستخدم
    // Return: حاجة - بس بتعمل save

    Task<bool> ExistsAsync(Guid id);
    // Method: هل المستخدم موجود؟
    // Return: true او false

    // ====== Related Entities ======

    Task AddProfileAsync(UserProfile profile);
    // Method: اضافة بروفايل للمستخدم

    Task AddWalletAsync(Wallet wallet);
    // Method: اضافة محفظة للمستخدم
}
```

---

### 7.3 ITokenService

**الموقع:** `src/DEPI.Domain/Shared/Interfaces/ITokenService.cs`

```csharp
namespace DEPI.Domain.Shared.Interfaces;

public class TokenResult
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? ExpiresAt { get; set; }
}

public interface ITokenService
{
    Task<TokenResult> GenerateTokenAsync(Guid userId, string email);
    // Method: عمل Token جديد
    // Parameters: userId, email
    // Return: TokenResult فيه Access Token و Refresh Token

    Task<TokenResult> RefreshTokenAsync(string refreshToken);
    // Method: تجديد Token باستخدام Refresh Token
    // Parameters: refreshToken
    // Return: TokenResult فيه Token جديد

    Task<bool> ValidateTokenAsync(string token);
    // Method: هل Token صحيح؟
    // Return: true او false

    Task<Guid?> GetUserIdFromTokenAsync(string token);
    // Method: جيب الـ UserId من Token
    // Return: UserId ولا null
}
```

---

## 8. Domain Events - احداث الدومين

### 8.1 ايه هو Domain Event؟

Domain Event هو رسالة بتتقال لما حاجة معينة تحصل في الكود.

### مثال:

```
User سجل ← UserRegisteredEvent
Project اتعمل ← ProjectCreatedEvent
Payment اتدفع ← PaymentProcessedEvent
```

---

### 8.2 لية بنستخدم Domain Events؟

1. **Decoupling** - فصل اجزاء الكود عن بعض
2. **Audit Trail** - تتبع كل حاجة حصلت
3. **Notifications** - تبعت اشعارات
4. **Side Effects** - عمليات جانبية من غير ما نكسر الكود

---

### 8.3 ازاي نعمل Domain Event:

#### A. IDomainEvent Interface

```csharp
namespace DEPI.Domain.Common.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
```

#### B. Base Domain Event

```csharp
namespace DEPI.Domain.Common.Events;

public abstract class DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; }

    protected DomainEvent()
    {
        OccurredOn = DateTime.UtcNow;
    }
}
```

#### C. Specific Events

```csharp
// User Registered Event
public class UserRegisteredEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public string FullName { get; }
    public DateTime RegisteredAt { get; }

    public UserRegisteredEvent(Guid userId, string email, string fullName)
    {
        UserId = userId;
        Email = email;
        FullName = fullName;
        RegisteredAt = DateTime.UtcNow;
    }
}

// Project Created Event
public class ProjectCreatedEvent : DomainEvent
{
    public Guid ProjectId { get; }
    public Guid OwnerId { get; }
    public string Title { get; }
    public decimal Budget { get; }

    public ProjectCreatedEvent(Guid projectId, Guid ownerId, string title, decimal budget)
    {
        ProjectId = projectId;
        OwnerId = ownerId;
        Title = title;
        Budget = budget;
    }
}
```

---

### 8.4 ازاي نرسل Domain Event من Entity:

```csharp
public class User : AuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public string Email { get; set; }
    public string FullName { get; set; }

    // Method لترسيل الحدث
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    // Method لتسجيل مستخدم جديد
    public static User Create(string email, string fullName)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            FullName = fullName
        };

        // Raise Domain Event
        user.RaiseDomainEvent(new UserRegisteredEvent(user.Id, user.Email, user.FullName));

        return user;
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
```

---

## 9. Relationships - العلاقات

### 9.1 انواع العلاقات

#### One-to-One - واحد لواحد

مثال: User له UserProfile واحد

```csharp
public class User : AuditableEntity
{
    public Guid? ProfileId { get; set; }
    public virtual UserProfile? Profile { get; set; }
}

public class UserProfile : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
```

#### One-to-Many - واحد لكثير

مثال: User له Sessions كتير

```csharp
public class User : AuditableEntity
{
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}

public class Session : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
```

#### Many-to-Many - كثير لكثير

مثال: User له Roles كتير، Role ليها Users كتير

```csharp
public class User : AuditableEntity
{
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

public class Role : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
}
```

---

### 9.2 Navigation Properties

دي الخصائص اللي بتستخدم للتنقل بين الكيانات:

```csharp
// Optional Single Navigation - تنقل لواحد (nullable)
public virtual Country? Country { get; set; }

// Required Single Navigation - تنقل لواحد (مش nullable)
public virtual User User { get; set; }

// Collection Navigation - تنقل لكتير
public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
```

---

## 10. نصائح مهمة

### 10.1 Entity Design Guidelines

1. **Entity لازم يكون له Id فريد**
2. **Entity بيتغير (mutable)**
3. **Entity بيتقارن بالـ Id**
4. **Entity ممكن يكون ليه Domain Events**

### 10.2 Value Object Design Guidelines

1. **Value Object ما لهش Id**
2. **Value Object ما بيتغيرش (immutable)**
3. **Value Object بيتقارن بالمحتوى**
4. **Value Object لازم يكون ليه Factory Method آمن**

### 10.3 Common Mistakes - غلطات شائعة

| غلط | صح |
|-----|-----|
| Validation في Entity | Validation في Application Layer |
| Database logic في Entity | Database logic في Infrastructure |
| HTTP stuff في Entity | HTTP stuff في API Layer |
| Dependency Injection في Entity | Dependencies في Application Layer |

### 10.4 Best Practices

1. **Keep Entities Simple** - خلي الكيانات بسيطة
2. **Use Value Objects for primitives** - استخدم Value Objects للبيانات البسيطة
3. **Use Enums for fixed values** - استخدم Enums للقيم الثابتة
4. **Define clear Interfaces** - عرف Interfaces واضحة
5. **Raise Domain Events for side effects** - ارفع Domain Events للعمليات الجانبية

---

## ملخص

### Domain Layer يتكون من:

```
Domain Layer
├── Common/
│   ├── Base/
│   │   ├── BaseEntity.cs        ← الكيان الاساسي
│   │   └── AuditableEntity.cs   ← الكيان مع التتبع
│   └── ValueObjects/
│       └── ValueObject.cs        ← كلاس Value Object الاساسي
├── Modules/
│   ├── Identity/
│   │   ├── Entities/            ← User, Session, Token, etc.
│   │   └── Enums/               ← UserType, VerificationStatus, etc.
│   ├── Projects/
│   │   ├── Entities/            ← Project, Proposal, Contract, etc.
│   │   └── Enums/               ← ProjectStatus, etc.
│   └── ... (كل الموديولز)
└── Shared/
    └── Interfaces/               ← IUserRepository, ITokenService, etc.
```

### المفتاح الاساسي:

> **Domain Layer = Business Model = "WHAT"**
>
> مش "HOW" - ازاي نعمل حاجة
> مش "WHEN" - امتى نعمل حاجة
> مش "WHERE" - فين هنعمل حاجة

> بس **"WHAT"** - هنمثّل ايه في المشروع

---

## الخطوة التالية

بعد ما تتعلم Domain Layer، الروحة لـ Application Layer عشان تتعلم ازاي:

1. تنشئ **UseCases**
2. تنشئ **DTOs**
3. تستخدم **Interfaces**
4. تتعامل مع **Domain Events**
