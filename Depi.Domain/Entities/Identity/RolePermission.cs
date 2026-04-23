namespace DEPI.Domain.Entities.Identity;

using DEPI.Domain.Common.Base;

public class RolePermission : AuditableEntity
{
    public Guid RoleId { get; private set; }
    public string Permission { get; private set; } = string.Empty;

    public virtual Role? Role { get; private set; }

    private RolePermission() { }

    public static RolePermission Create(Guid roleId, string permission)
    {
        return new RolePermission
        {
            RoleId = roleId,
            Permission = permission
        };
    }
}