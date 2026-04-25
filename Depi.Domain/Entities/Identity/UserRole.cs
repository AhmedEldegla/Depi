using Microsoft.AspNetCore.Identity;

namespace DEPI.Domain.Entities.Identity;

public class UserRole : IdentityUserRole<Guid>
{
    public string? AssignedBy { get; private set; }
    public DateTime AssignedAt { get; private set; }

    public virtual User? User { get; private set; }
    public virtual Role? Role { get; private set; }

    public UserRole() : base() { }

    public static UserRole Create(Guid userId, Guid roleId, string? assignedBy = null)
    {
        return new UserRole
        {
            UserId = userId,
            RoleId = roleId,
            AssignedBy = assignedBy,
            AssignedAt = DateTime.UtcNow
        };
    }
}