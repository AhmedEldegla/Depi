namespace DEPI.Domain.Entities.Identity;

using Depi.Domain.Modules.Identity.Enums;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Common.Events;
using DEPI.Domain.Modules.Identity.Entities;

public class User : AuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public string Email { get;set; } = string.Empty;
    public string PasswordHash { get;set; } = string.Empty;
    public string PasswordSalt { get;set; } = string.Empty;
    public string FirstName { get;set; } = string.Empty;
    public string LastName { get;set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public UserType UserType { get;set; }
    public UserStatus Status { get;set; } = UserStatus.Pending;
    public Gender Gender { get;set; }
    public DateTime? DateOfBirth { get;set; }
    public string? PhoneNumber { get;set; }
    public string? ProfileImageUrl { get;set; }
    public int? CountryId { get;set; }
    public bool IsEmailVerified { get;set; }
    public bool IsPhoneVerified { get;set; }
    public bool IsTwoFactorEnabled { get;set; }
    public string? TwoFactorSecret { get;set; }
    public DateTime? LastLoginAt { get;set; }
    public string? LastLoginIpAddress { get;set; }
    public int FailedLoginAttempts { get;set; }
    public DateTime? LockoutEndAt { get;set; }
    public string? RefreshToken { get;set; }
    public DateTime? RefreshTokenExpiry { get;set; }
    public string? ResetToken { get;set; }
    public DateTime? ResetTokenExpiry { get;set; }
    public int SecurityStamp { get;set; }

    public ICollection<UserRole> UserRoles { get;set; } = new HashSet<UserRole>();
    public ICollection<Session> Sessions { get;set; } = new HashSet<Session>();
    public ICollection<SecurityLog> SecurityLogs { get;set; } = new HashSet<SecurityLog>();

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
            throw new ArgumentException("Email cannot be empty", nameof(email));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty", nameof(passwordHash));

        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty", nameof(lastName));

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
            user.FullName,
            string.Empty,
            string.Empty));

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
            throw new InvalidOperationException("Cannot update a deleted user");

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Gender = gender;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber?.Trim();
        CountryId = countryId;
        SecurityStamp = Random.Shared.Next();
    }

    public void UpdateActivity()
    {
        LastLoginAt = DateTime.UtcNow;
    }

    public void ChangePassword(string newPasswordHash, string newPasswordSalt)
    {
        if (IsDeleted)
            throw new InvalidOperationException("Cannot change password for a deleted user");

        PasswordHash = newPasswordHash;
        PasswordSalt = newPasswordSalt;
        SecurityStamp = Random.Shared.Next();

        InvalidateAllRefreshTokens();
    }

    public bool VerifyPassword(string passwordHash, string passwordSalt)
    {
        return PasswordHash == passwordHash && PasswordSalt == passwordSalt;
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
            throw new InvalidOperationException("Cannot verify email for a deleted user");

        IsEmailVerified = true;

        if (Status == UserStatus.Pending)
        {
            Status = UserStatus.Active;
        }
    }

    public void VerifyPhone()
    {
        if (IsDeleted)
            throw new InvalidOperationException("Cannot verify phone for a deleted user");

        IsPhoneVerified = true;
    }

    public void EnableTwoFactor(string secret)
    {
        if (IsDeleted)
            throw new InvalidOperationException("Cannot enable two factor for a deleted user");

        TwoFactorSecret = secret;
        IsTwoFactorEnabled = true;
        SecurityStamp = Random.Shared.Next();
    }

    public void DisableTwoFactor()
    {
        if (IsDeleted)
            throw new InvalidOperationException("Cannot disable two factor for a deleted user");

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
            throw new InvalidOperationException("Cannot set reset token for a deleted user");

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

    public void Suspend(string reason)
    {
        if (IsDeleted)
            throw new InvalidOperationException("Cannot suspend a deleted user");

        Status = UserStatus.Suspended;
        SecurityStamp = Random.Shared.Next();
    }

    public void Ban(string reason)
    {
        if (IsDeleted)
            throw new InvalidOperationException("Cannot ban a deleted user");

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
        if (Status == UserStatus.Deactivated)
            throw new InvalidOperationException("Cannot reactivate a deactivated user");

        Status = UserStatus.Active;
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

    public bool HasRole(string roleName)
    {
        return UserRoles.Any(ur => ur.Role.Name == roleName);
    }

    public bool HasPermission(string permissionName)
    {
        return UserRoles.SelectMany(ur => ur.Role.RolePermissions)
            .Any(rp => rp.Permission.Name == permissionName);
    }
}
