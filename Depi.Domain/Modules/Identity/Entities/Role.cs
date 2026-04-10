namespace DEPI.Domain.Modules.Identity.Entities;

using DEPI.Domain.Common.Base;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
}
