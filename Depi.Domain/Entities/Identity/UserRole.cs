namespace DEPI.Domain.Entities.Identity;

using DEPI.Domain.Common.Base;

public class UserRole : AuditableEntity
{
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    public virtual User? User { get; private set; }
    public virtual Role? Role { get; private set; }

    private UserRole() { }

    public static UserRole Create(Guid userId, Guid roleId)
    {
        return new UserRole
        {
            UserId = userId,
            RoleId = roleId
        };
    }
}