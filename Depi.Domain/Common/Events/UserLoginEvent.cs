namespace DEPI.Domain.Common.Events;

public sealed class UserLoginEvent : DomainEventBase
{
    public Guid UserId { get; }
    public string Email { get; }
    public bool IsSuccessful { get; }
    public string IpAddress { get; }
    public string UserAgent { get; }
    public string? FailureReason { get; }

    public override string EventType => nameof(UserLoginEvent);

    public UserLoginEvent(
        Guid userId,
        string email,
        bool isSuccessful,
        string ipAddress,
        string userAgent,
        string? failureReason = null)
    {
        UserId = userId;
        Email = email;
        IsSuccessful = isSuccessful;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        FailureReason = failureReason;
    }
}
