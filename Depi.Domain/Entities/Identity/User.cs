namespace DEPI.Domain.Entities.Identity;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Common.Events;
using DEPI.Domain.Enums;

public class User : AuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string PasswordSalt { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public UserType UserType { get; private set; }
    public UserStatus Status { get; private set; } = UserStatus.Pending;
    public Gender Gender { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? ProfileImageUrl { get; private set; }
    public int? CountryId { get; private set; }
    public bool IsEmailVerified { get; private set; }
    public bool IsPhoneVerified { get; private set; }
    public bool IsIdentityVerified { get; private set; }
    public bool IsTwoFactorEnabled { get; private set; }
    public string? EmailVerificationToken { get; private set; }
    public DateTime? EmailVerificationTokenExpiry { get; private set; }
    public string? PhoneVerificationCode { get; private set; }
    public DateTime? PhoneVerificationCodeExpiry { get; private set; }
    public string? TwoFactorSecret { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public string? LastLoginIpAddress { get; private set; }
    public int FailedLoginAttempts { get; private set; }
    public DateTime? LockoutEndAt { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiry { get; private set; }
    public string? ResetToken { get; private set; }
    public DateTime? ResetTokenExpiry { get; private set; }
    public int SecurityStamp { get; private set; }

    public virtual ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
    public virtual ICollection<UserSession> Sessions { get; private set; } = new List<UserSession>();

    private User() { }

    public static User Create(
        string email,
        string passwordHash,
        string passwordSalt,
        string firstName,
        string lastName,
        UserType userType,
        Gender gender = Gender.NotSpecified,
        DateTime? dateOfBirth = null,
        string? phoneNumber = null,
        int? countryId = null)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email مطلوب", nameof(email));
            
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash مطلوب", nameof(passwordHash));
            
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("الاسم الأول مطلوب", nameof(firstName));
            
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("الاسم الأخير مطلوب", nameof(lastName));

        var user = new User
        {
            Email = email.ToLowerInvariant().Trim(),
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            UserType = userType,
            Gender = gender,
            DateOfBirth = dateOfBirth,
            PhoneNumber = phoneNumber?.Trim(),
            CountryId = countryId,
            Status = UserStatus.Pending,
            SecurityStamp = Random.Shared.Next()
        };

        user.RaiseDomainEvent(new UserRegisteredEvent(
            user.Id,
            user.Email,
            user.FullName));

        return user;
    }

    public void UpdateProfile(
        string firstName,
        string lastName,
        Gender gender,
        DateTime? dateOfBirth,
        string? phoneNumber,
        int? countryId)
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن تحديث مستخدم محذوف");
            
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Gender = gender;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber?.Trim();
        CountryId = countryId;
        SecurityStamp = Random.Shared.Next();
    }

    public void ChangePassword(string newPasswordHash, string newPasswordSalt)
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن تغيير كلمة المرور");
            
        PasswordHash = newPasswordHash;
        PasswordSalt = newPasswordSalt;
        SecurityStamp = Random.Shared.Next();
        InvalidateAllRefreshTokens();
    }

    public bool VerifyPassword(string hash, string salt)
    {
        return PasswordHash == hash && PasswordSalt == salt;
    }

    public void RecordSuccessfulLogin(string ipAddress)
    {
        LastLoginAt = DateTime.UtcNow;
        LastLoginIpAddress = ipAddress;
        FailedLoginAttempts = 0;
        LockoutEndAt = null;
    }

    public void RecordFailedLogin()
    {
        FailedLoginAttempts++;
        
        if (FailedLoginAttempts >= 5)
        {
            LockoutEndAt = DateTime.UtcNow.AddMinutes(15);
        }
    }

    public bool IsLockedOut => LockoutEndAt.HasValue && LockoutEndAt > DateTime.UtcNow;

    public void VerifyEmail()
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن التحقق من بريد مستخدم محذوف");
            
        IsEmailVerified = true;
        
        if (Status == UserStatus.Pending)
        {
            Status = UserStatus.Active;
        }
    }

    public void VerifyPhone()
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن التحقق من هاتف مستخدم محذوف");
            
        IsPhoneVerified = true;
    }

    public void VerifyIdentity()
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن التحقق من هوية مستخدم محذوف");
            
        IsIdentityVerified = true;
        Status = UserStatus.Active;
    }

    public void GenerateEmailVerificationToken()
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن إنشاء رمز التحقق");
            
        EmailVerificationToken = Guid.NewGuid().ToString("N");
        EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24);
    }

    public void GeneratePhoneVerificationCode()
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن إنشاء رمز التحقق");
            
        if (string.IsNullOrWhiteSpace(PhoneNumber))
            throw new InvalidOperationException("رقم الهاتف مطلوب");
            
        PhoneVerificationCode = Random.Shared.Next(100000, 999999).ToString();
        PhoneVerificationCodeExpiry = DateTime.UtcNow.AddMinutes(10);
    }

    public void ConfirmEmailVerification(string token)
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن تأكيد التحقق");
            
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("رمز التحقق مطلوب");
            
        if (EmailVerificationToken != token)
            throw new InvalidOperationException("رمز التحقق غير صحيح");
            
        if (!EmailVerificationTokenExpiry.HasValue || EmailVerificationTokenExpiry < DateTime.UtcNow)
            throw new InvalidOperationException("انتهت صلاحية رمز التحقق");
            
        IsEmailVerified = true;
        EmailVerificationToken = null;
        EmailVerificationTokenExpiry = null;
    }

    public void ConfirmPhoneVerification(string code)
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن تأكيد التحقق");
            
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("الكود مطلوب");
            
        if (PhoneVerificationCode != code)
            throw new InvalidOperationException("الكود غير صحيح");
            
        if (!PhoneVerificationCodeExpiry.HasValue || PhoneVerificationCodeExpiry < DateTime.UtcNow)
            throw new InvalidOperationException("انتهت صلاحية الكود");
            
        IsPhoneVerified = true;
        PhoneVerificationCode = null;
        PhoneVerificationCodeExpiry = null;
    }

    public void EnableTwoFactor(string secret)
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن تفعيل التحقق بخطوتين");
            
        TwoFactorSecret = secret;
        IsTwoFactorEnabled = true;
        SecurityStamp = Random.Shared.Next();
    }

    public void DisableTwoFactor()
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن إلغاء التحقق بخطوتين");
            
        TwoFactorSecret = null;
        IsTwoFactorEnabled = false;
        SecurityStamp = Random.Shared.Next();
    }

    public void SetRefreshToken(string token, DateTime expiry)
    {
        RefreshToken = token;
        RefreshTokenExpiry = expiry;
    }

    public void InvalidateRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiry = null;
    }

    public void InvalidateAllRefreshTokens()
    {
        RefreshToken = null;
        RefreshTokenExpiry = null;
        SecurityStamp = Random.Shared.Next();
    }

    public void SetResetToken(string token, DateTime expiry)
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن تعيين رمز إعادة التعيين");
            
        ResetToken = token;
        ResetTokenExpiry = expiry;
    }

    public bool ValidateResetToken(string token)
    {
        if (string.IsNullOrEmpty(ResetToken))
            return false;
            
        if (ResetTokenExpiry.HasValue && ResetTokenExpiry < DateTime.UtcNow)
            return false;
            
        return ResetToken == token;
    }

    public void ClearResetToken()
    {
        ResetToken = null;
        ResetTokenExpiry = null;
    }

    public void Suspend()
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن إيقاف مستخدم محذوف");
            
        Status = UserStatus.Suspended;
        SecurityStamp = Random.Shared.Next();
    }

    public void Ban()
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن حظر مستخدم محذوف");
            
        Status = UserStatus.Banned;
        InvalidateAllRefreshTokens();
        SecurityStamp = Random.Shared.Next();
    }

    public void Deactivate(Guid performedBy)
    {
        Status = UserStatus.Deactivated;
        InvalidateAllRefreshTokens();
        MarkAsDeleted(performedBy);
    }

    public void Reactivate()
    {
        if (Status != UserStatus.Deactivated)
            throw new InvalidOperationException("لا يمكن إعادة تفعيل");
            
        Status = UserStatus.Active;
        UndoDelete();
        SecurityStamp = Random.Shared.Next();
    }

    public void SetProfileImage(string imageUrl)
    {
        ProfileImageUrl = imageUrl;
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

public class UserRegisteredEvent : DomainEventBase
{
    public Guid UserId { get; }
    public string Email { get; }
    public string FullName { get; }

    public override string EventType => nameof(UserRegisteredEvent);

    public UserRegisteredEvent(Guid userId, string email, string fullName)
    {
        UserId = userId;
        Email = email;
        FullName = fullName;
    }
}