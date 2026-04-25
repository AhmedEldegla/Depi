namespace DEPI.Domain.Entities.Identity;

using DEPI.Domain.Common.Messages;
using Microsoft.AspNetCore.Identity;

public class Role : IdentityRole<Guid>
{
    public string? Description { get; private set; }
    public bool IsDefault { get; private set; }
    
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
    public virtual ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();

    private Role() : base() { }

    public static Role Create(string name, string? description = null, bool isDefault = false)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(Strings.Validation.RoleNameRequired, nameof(name));

        return new Role
        {
            Name = name.Trim(),
            NormalizedName = name.ToUpperInvariant().Trim(),
            Description = description?.Trim(),
            IsDefault = isDefault,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(Strings.Validation.RoleNameRequired, nameof(name));

        Name = name.Trim();
        NormalizedName = name.ToUpperInvariant().Trim();
        Description = description?.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetAsDefault()
    {
        IsDefault = true;
    }
}