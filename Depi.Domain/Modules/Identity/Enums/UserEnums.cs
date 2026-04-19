namespace Depi.Domain.Modules.Identity.Enums;

public enum UserType
{
    Freelancer = 1,
    Client = 2,
    Admin = 3
}

public enum UserStatus
{
    Pending = 1,
    Active = 2,
    Suspended = 3,
    Banned = 4,
    Deactivated = 5
}

public enum Gender
{
    NotSpecified = 0,
    Male = 1,
    Female = 2,
    Other = 3
}
