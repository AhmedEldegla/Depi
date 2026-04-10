namespace DEPI.Domain.Modules.Identity.Entities;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Enums;

public class Token : BaseEntity
{
    public Guid? UserId { get; set; }
    public TokenType? TokenType { get; set; }
    public string TokenHash { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedAt { get; set; }
    public User? User { get; set; }
}
