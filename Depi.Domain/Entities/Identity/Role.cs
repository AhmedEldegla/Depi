namespace DEPI.Domain.Entities.Identity;

using DEPI.Domain.Common.Base;

public class Role : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool IsDefault { get; private set; }

    public virtual ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
    public virtual ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();

    private Role() { }

    public static Role Create(string name, string description = "", bool isDefault = false)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("اسم الدور مطلوب", nameof(name));

        return new Role
        {
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty,
            IsDefault = isDefault
        };
    }

    public void Update(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("اسم الدور مطلوب", nameof(name));

        Name = name.Trim();
        Description = description?.Trim() ?? string.Empty;
    }
}