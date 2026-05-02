namespace DEPI.Domain.Entities.Identity;

using DEPI.Domain.Common.Events;
using DEPI.Domain.Common.Messages;
using DEPI.Domain.Enums;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public UserType UserType { get; private set; }
    public UserStatus Status { get; private set; } = UserStatus.Pending;
    public Gender Gender { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public int? CountryId { get; private set; }
    public bool IsIdentityVerified { get; private set; }
    public string? ProfileImageUrl { get; private set; }
    public string? LastLoginIpAddress { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiry { get; private set; }

    public virtual ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
    public virtual ICollection<UserSession> Sessions { get; private set; } = new List<UserSession>();

    private User() : base() { }

    public static User Create(
        string email,
        string userName,
        string firstName,
        string lastName,
        UserType userType,
        Gender gender = Gender.NotSpecified,
        DateTime? dateOfBirth = null,
        string? phoneNumber = null,
        int? countryId = null)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException(Strings.Validation.EmailRequired, nameof(email));
            
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException(Strings.Validation.UserNameRequired, nameof(userName));
            
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException(Strings.Validation.FirstNameRequired, nameof(firstName));
            
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException(Strings.Validation.LastNameRequired, nameof(lastName));

        var user = new User
        {
            Email = email.ToLowerInvariant().Trim(),
            UserName = userName.ToLowerInvariant().Trim(),
            NormalizedEmail = email.ToUpperInvariant().Trim(),
            NormalizedUserName = userName.ToUpperInvariant().Trim(),
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            UserType = userType,
            Gender = gender,
            DateOfBirth = dateOfBirth,
            PhoneNumber = phoneNumber?.Trim(),
            CountryId = countryId,
            Status = UserStatus.Pending,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            AccessFailedCount = 0,
            LockoutEnd = null,
            CreatedAt = DateTime.UtcNow
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
            throw new InvalidOperationException(Strings.Errors.CannotUpdateDeleted);
            
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Gender = gender;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber?.Trim();
        CountryId = countryId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateLastLogin(string? ipAddress = null)
    {
        LastLoginAt = DateTime.UtcNow;
        LastLoginIpAddress = ipAddress;
    }

    public void VerifyIdentity()
    {
        if (IsDeleted)
            throw new InvalidOperationException(Strings.Errors.CannotVerifyDeletedUser);
            
        IsIdentityVerified = true;
        
        if (Status == UserStatus.Pending)
        {
            Status = UserStatus.Active;
        }
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

    public void Suspend()
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن إيقاف مستخدم محذوف");
            
        Status = UserStatus.Suspended;
    }

    public void Ban()
    {
        if (IsDeleted)
            throw new InvalidOperationException("لا يمكن حظر مستخدم محذوف");
            
        Status = UserStatus.Banned;
        InvalidateRefreshToken();
    }

    public void Deactivate(Guid performedBy)
    {
        Status = UserStatus.Deactivated;
        InvalidateRefreshToken();
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeletedBy = performedBy;
    }

    public void Reactivate()
    {
        if (Status != UserStatus.Deactivated)
            throw new InvalidOperationException("لا يمكن إعادة تفعيل");
            
        Status = UserStatus.Active;
        IsDeleted = false;
        DeletedAt = null;
        DeletedBy = null;
    }

    public void SetProfileImage(string imageUrl)
    {
        ProfileImageUrl = imageUrl;
    }

public bool HasRole(string roleName)
    {
        return UserRoles.Any(ur => ur.RoleId != Guid.Empty);
    }

    public void EnsureVerifiedFor(string action)
    {
        if (!IsIdentityVerified)
            throw new InvalidOperationException(
                $"يجب توثيق هويتك قبل {action}");
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