namespace DEPI.Application.Common;

public enum ErrorCode
{
    None = 0,

    NotFound = 1000,
    ValidationError = 1001,
    Unauthorized = 1002,
    Forbidden = 1003,
    InternalError = 1004,
    Conflict = 1005,
    BadRequest = 1006,
    IdentityNotVerified = 1007,

    ProjectNotFound = 2000,
    ProjectTitleRequired = 2001,
    ProjectBudgetInvalid = 2002,
    ProjectNotEditable = 2003,

    ProposalNotFound = 3000,
    ProposalAlreadySubmitted = 3001,
    ProposalNotEditable = 3002,

    ContractNotFound = 4000,
    ContractAlreadyExists = 4001,

    UserNotFound = 5000,
    UserEmailExists = 5001,
    UserInvalidCredentials = 5002,
    UserEmailInvalid = 5003,

    WalletNotFound = 6000,
    InsufficientBalance = 6001,
}

public static class Errors
{
    public static string NotFound(string entity)
        => $"{entity} not found";

    public static string AlreadyExists(string entity)
        => $"{entity} already exists";

    public static string Validation(string field, string message)
        => $"Validation error for {field}: {message}";

    public static string Required(string field)
        => $"{field} is required";

    public static string Unauthorized()
        => "You must be logged in";

    public static string Forbidden()
        => "You don't have permission";

    public static string Internal()
        => "An internal error occurred";

    public static string InvalidCredentials()
        => "Invalid email or password";

    public static string IdentityNotVerified(string action)
        => $"يجب توثيق هويتك قبل {action}";
}