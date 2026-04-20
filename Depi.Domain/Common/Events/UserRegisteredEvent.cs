namespace DEPI.Domain.Common.Events;

public sealed class UserRegisteredEvent : DomainEventBase
{
    public Guid UserId { get; }
    public string Email { get; }
    public string FullName { get; }
    public string IpAddress { get; }
    public string UserAgent { get; }

    public override string EventType => nameof(UserRegisteredEvent);

    public UserRegisteredEvent(
        Guid userId,
        string email,
        string fullName,
        string ipAddress,
        string userAgent)
    {
        UserId = userId;
        Email = email;
        FullName = fullName;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }
}
