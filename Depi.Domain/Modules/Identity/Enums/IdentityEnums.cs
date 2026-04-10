namespace DEPI.Domain.Modules.Identity.Enums;

public enum UserType
{
    Freelancer = 1,
    Client = 2,
    CompanyOwner = 3,
    Instructor = 4,
    HeadHunter = 5,
    Admin = 6,
    Student = 7
}

public enum TokenType
{
    Verification = 1,
    Reset = 2,
    OTP = 3,
    AccessToken = 4,
    RefreshToken = 5
}

public enum SecurityEventType
{
    LoginSuccess = 1,
    LoginFailed = 2,
    PasswordChanged = 3,
    EmailVerified = 4,
    AccountLocked = 5,
    AccountUnlocked = 6,
    SuspiciousActivity = 7
}
