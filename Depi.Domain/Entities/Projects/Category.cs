namespace DEPI.Domain.Entities.Projects;

using DEPI.Domain.Common.Base;

public class Category : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Icon { get; private set; } = string.Empty;
    public Guid? ParentCategoryId { get; private set; }
    public bool IsActive { get; private set; } = true;
    public int SortOrder { get; private set; }

    public virtual Category? ParentCategory { get; private set; }
    public virtual ICollection<Category> SubCategories { get; private set; } = new List<Category>();
    public virtual ICollection<Project> Projects { get; private set; } = new List<Project>();

    private Category() { }

    public static Category Create(string name, string description = "", string icon = "")
    {
        return new Category
        {
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty,
            Icon = icon?.Trim() ?? string.Empty,
            IsActive = true
        };
    }

    public void Update(string name, string description, string icon)
    {
        Name = name.Trim();
        Description = description?.Trim() ?? string.Empty;
        Icon = icon?.Trim() ?? string.Empty;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}