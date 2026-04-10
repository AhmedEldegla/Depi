namespace DEPI.Domain.Modules.Identity.Entities;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Enums;

public class SecurityLog : BaseEntity
{
    public Guid? UserId { get; set; }
    public SecurityEventType? EventType { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? IpAddress { get; set; }
    public string? Details { get; set; }
    public User? User { get; set; }
}
