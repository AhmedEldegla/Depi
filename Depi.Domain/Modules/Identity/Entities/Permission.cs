namespace DEPI.Domain.Modules.Identity.Entities;

using DEPI.Domain.Common.Base;

public class Permission : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string ModuleKey { get; set; } = string.Empty;
    public string ActionKey { get; set; } = string.Empty;

    public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
}
