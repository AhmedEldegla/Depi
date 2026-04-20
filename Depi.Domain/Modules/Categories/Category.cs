namespace DEPI.Domain.Entities.Categories;

using DEPI.Domain.Common.Base;

public class Category : BaseEntity
{
    public string Name { get;set; } = string.Empty;
    public string Slug { get;set; } = string.Empty;
    public string? Description { get;set; }
    public string? IconName { get;set; }
    public string? ImageUrl { get;set; }
    public int DisplayOrder { get;set; }
    public bool IsActive { get;set; }
    public int ProjectsCount { get;set; }
    public Guid? ParentCategoryId { get;set; }

    public virtual Category? ParentCategory { get;set; }
    public ICollection<Category> SubCategories { get;set; } = new HashSet<Category>();
    public ICollection<DEPI.Domain.Entities.Projects.Project> Projects { get;set; } = new HashSet<DEPI.Domain.Entities.Projects.Project>();

    private Category() { }

    public static Category Create(
        string name,
        string slug,
        string? description = null,
        string? iconName = null,
        int displayOrder = 0,
        bool isActive = true)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name is required", nameof(name));

        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Category slug is required", nameof(slug));

        if (!IsValidSlug(slug))
            throw new ArgumentException("Invalid slug format", nameof(slug));

        return new Category
        {
            Name = name.Trim(),
            Slug = slug.Trim().ToLowerInvariant(),
            Description = description?.Trim(),
            IconName = iconName?.Trim(),
            DisplayOrder = displayOrder,
            IsActive = isActive,
            ProjectsCount = 0
        };
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name is required", nameof(name));

        Name = name.Trim();
    }

    public void UpdateSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Category slug is required", nameof(slug));

        if (!IsValidSlug(slug))
            throw new ArgumentException("Invalid slug format", nameof(slug));

        Slug = slug.Trim().ToLowerInvariant();
    }

    public void UpdateDescription(string? description)
    {
        Description = description?.Trim();
    }

    public void UpdateIcon(string? iconName)
    {
        IconName = iconName?.Trim();
    }

    public void UpdateDisplayOrder(int order)
    {
        DisplayOrder = order;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void SetParentCategory(Guid? parentId)
    {
        if (parentId.HasValue && parentId.Value == Id)
            throw new InvalidOperationException("Category cannot be its own parent");

        ParentCategoryId = parentId;
    }

    public void IncrementProjectsCount()
    {
        ProjectsCount++;
    }

    public void DecrementProjectsCount()
    {
        if (ProjectsCount > 0)
            ProjectsCount--;
    }

    private static bool IsValidSlug(string slug)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(slug, @"^[a-z0-9]+(?:-[a-z0-9]+)*$");
    }
}
