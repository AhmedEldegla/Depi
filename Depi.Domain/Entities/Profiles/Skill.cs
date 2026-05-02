namespace DEPI.Domain.Entities.Profiles;

using DEPI.Domain.Common.Base;

public class Skill : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string NameEn { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsVerified { get; private set; }
    public bool IsActive { get; private set; } = true;
    public int DisplayOrder { get; private set; }

    private Skill() { }

    public static Skill Create(
        string name,
        string nameEn,
        string? description = null,
        bool isVerified = false,
        int displayOrder = 0)
    {
        return new Skill
        {
            Name = name,
            NameEn = nameEn,
            Description = description,
            IsVerified = isVerified,
            IsActive = true,
            DisplayOrder = displayOrder
        };
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("المهارة غير نشطة بالفعل");

        IsActive = false;
    }

public void Activate()
    {
        if (IsActive)
            throw new InvalidOperationException("المهارة نشط بالفعل");

        IsActive = true;
    }

    public void UpdateInfo(string name, string nameEn, string? description)
    {
        Name = name;
        NameEn = nameEn;
        Description = description;
    }
}