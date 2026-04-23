namespace DEPI.Domain.Entities.Identity;

using DEPI.Domain.Common.Base;

public class Permission : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;

    public virtual ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();

    private Permission() { }

    public static Permission Create(string name, string description = "", string category = "")
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("اسم الأذون مطلوب", nameof(name));

        return new Permission
        {
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty,
            Category = category?.Trim() ?? string.Empty
        };
    }

    public void Update(string name, string description, string category)
    {
        Name = name.Trim();
        Description = description?.Trim() ?? string.Empty;
        Category = category?.Trim() ?? string.Empty;
    }
}